using LibaryDatabase.Model;
using LibraryCore.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryApiTemplate.Repos
{
    public abstract class RepoQueryble<TDbContext> : IRepoQueryble where TDbContext : DbContext
    {
        IDbContextFactory<TDbContext> _dbContextFactory;

        public RepoQueryble(IDbContextFactory<TDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public IQueryable<TEntity> SelectAllRecord<TEntity>() where TEntity : class, IDbRecord<TEntity>, new()
        {
            try
            {
                var dbContext = _dbContextFactory.CreateDbContext();
                DbSet<TEntity> dbSet = dbContext.GetDbSet<TEntity>();

                return dbSet.AsNoTracking();
            }
            catch (Exception ex)
            {
                LibraryLogging.LoggingBroker.LogError($"{nameof(RepoQueryble<TDbContext>)} osztályban, {nameof(SelectAllRecord)} metódusba kivétel keletkezett!\n{ex.Message}");
                return Enumerable.Empty<TEntity>().AsQueryable();
            }
        }

        public async Task<TEntity> GetById<TEntity>(Guid id) where TEntity : class, IDbRecord<TEntity>, new()
        {
            try
            {
                var dbContext = _dbContextFactory.CreateDbContext();
                DbSet<TEntity> dbSet = dbContext.GetDbSet<TEntity>();
               
                return await dbSet.AsNoTracking<TEntity>().FirstOrDefaultAsync<TEntity>(entity => entity.Id == id) ?? new TEntity();
            }
            catch(Exception ex)
            {
                LibraryLogging.LoggingBroker.LogError($"{nameof(RepoQueryble<TDbContext>)} osztályban, {nameof(SelectAllRecord)} metódusba kivétel keletkezett!\n{ex.Message}");
                return new TEntity();
            }
        }
    }
}
