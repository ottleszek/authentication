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

        public async Task<List<TEntity>> SelectAllRecordIncludedAsync<TEntity>() where TEntity : class, IDbRecord<TEntity>, new()
        {
            IQueryable<TEntity>? entities = GetAllIncluded<TEntity>();
            List<TEntity> list = new List<TEntity>();
            if (entities is not null)
            {
                list=await entities.ToListAsync();
            }
            return list;
        }

        protected abstract IQueryable<TEntity>? GetAllIncluded<TEntity>() where TEntity : class, new();


    }
}
