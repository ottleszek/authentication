using LibraryCore.Model;
using LibraryDataBroker;
using Microsoft.EntityFrameworkCore;

namespace LibraryApiTemplate.Repos
{
    public abstract class RepoIncluded<TDbContext> : RepoList<TDbContext>, IIncludedDataBroker where TDbContext : DbContext
    {
        private readonly IDbContextFactory<TDbContext> _dbContextFactory;

        public RepoIncluded(IDbContextFactory<TDbContext> dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Task<List<TEntity>> SelectAllRecordIncludedAsync<TEntity>() where TEntity : class, IDbRecord<TEntity>, new()
        {
            IQueryable<TEntity>? entitise = GetAllIncluded<TEntity>();
            List<TEntity> list = new List<TEntity>();
            if (entitise is not null)
            {
                list=entitise.ToList();
            }
            return list;
        }

        protected abstract IQueryable<TEntity>? GetAllIncluded<TEntity>() where TEntity : class, new();


    }
}
