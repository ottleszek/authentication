using LibraryCore.Model;
using LibraryCore.Responses;
using LibraryDatabase.Model;
using LibraryDataBroker;
using Microsoft.EntityFrameworkCore;

namespace LibraryApiTemplate.Repos
{
    public class RepoCrud<TDbContext> : RepoUpdate<TDbContext>, ICrudDataBroker where TDbContext : DbContext
    {
        private readonly IDbContextFactory<TDbContext> _dbContextFactory;

        public RepoCrud(IDbContextFactory<TDbContext> dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<ControllerResponse> DeleteAsync<TEntity>(Guid id) where TEntity : class, IDbRecord<TEntity>, new()
        {
            ControllerResponse response = new ControllerResponse();
            var dbContext = _dbContextFactory.CreateDbContext();
            var dbSet = dbContext.GetDbSet<TEntity>();
            TEntity entityToDelete = await GetByIdAsnyc<TEntity>(id);

            if (entityToDelete == null || entityToDelete == default)
            {
                LibraryLogging.LoggingBroker.LogError($"{nameof(RepoCrud<TDbContext>)}\nA törlendő adat nem található:\nAdat id:{id}");
                response.ClearAndAddError($"Az adat nem törölhető!");

            }
            else
            {
                try
                {
                    dbContext.ChangeTracker.Clear();
                    dbContext.Entry(entityToDelete).State = EntityState.Deleted;
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    LibraryLogging.LoggingBroker.LogError($"{nameof(RepoCrud<TDbContext>)}\nSql utasítás nem hajtható végre!\n{ex.Message}");
                    response.ClearAndAddError("Az adat nem törölhető!");
                }
            }
            return response;
        }

        public async Task<ControllerResponse> InsertAsync<TEntity>(TEntity entity) where TEntity : class, IDbRecord<TEntity>, new()
        {
            if (((IDbRecord<TEntity>)entity).HasId)
            {
                return await UpdateAsync<TEntity>(entity);
            }
            else
            {
                return await InsertNewItemAsync<TEntity>(entity);
            }
        }

        private async Task<ControllerResponse> InsertNewItemAsync<TEntity>(TEntity entity) where TEntity : class, IDbRecord<TEntity>, new()
        {
            ControllerResponse response=new ControllerResponse();
            var dbContext = _dbContextFactory.CreateDbContext();
            var dbSet = dbContext.GetDbSet<TEntity>();

            try
            {
                dbSet.Add(entity);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                LibraryLogging.LoggingBroker.LogError($"{nameof(RepoCrud<TDbContext>)}\nSql utasítás nem hajtható végre!\n{ex.Message}");
                response.ClearAndAddError($"Az új adat nem menthető!");
            }
            return response;
        }
    }
}
