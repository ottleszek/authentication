using LibraryCore.Model;
using LibraryDataBrokerProject;
using Microsoft.EntityFrameworkCore;

namespace LibraryApiTemplate.Repos
{
    public class RepoList<TDbContext> : RepoQueryble<TDbContext>, IListDataBroker where TDbContext : DbContext
    {
        IDbContextFactory<TDbContext> _dbContextFactory;

        public RepoList(IDbContextFactory<TDbContext> dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<TEntity>> SelectAllRecordAsync<TEntity>() where TEntity : class, IDbRecord<TEntity>, new()
        {
            return await SelectAllRecord<TEntity>().ToListAsync();
        }
    }
}
