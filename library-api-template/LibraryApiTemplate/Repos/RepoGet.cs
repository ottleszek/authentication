using LibraryCore.Model;
using LibraryDataBroker;
using Microsoft.EntityFrameworkCore;

namespace LibraryApiTemplate.Repos
{
    public class RepoGet<TDbContext> : RepoQueryble<TDbContext>, IGetDataBroker where TDbContext : DbContext
    {
        IDbContextFactory<TDbContext> _dbContextFactory;

        public RepoGet(IDbContextFactory<TDbContext> dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<TEntity> GetByIdAsnyc<TEntity>(Guid id) where TEntity : class, IDbRecord<TEntity>, new()
        {
            return await GetById<TEntity>(id);
        }
    }
}
