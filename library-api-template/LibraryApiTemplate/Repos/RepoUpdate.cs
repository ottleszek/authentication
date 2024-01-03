using LibraryCore.Model;
using LibraryCore.Responses;
using LibraryDataBroker;
using Microsoft.EntityFrameworkCore;

namespace LibraryApiTemplate.Repos
{
    public class RepoUpdate<TDbContext> : RepoGet<TDbContext>, IUpdateDataBroker where TDbContext : DbContext
    {
        private readonly IDbContextFactory<TDbContext> _dbContextFactory;

        public RepoUpdate(IDbContextFactory<TDbContext> dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<ControllerResponse>  UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IDbRecord<TEntity>, new()
        {
            ControllerResponse response = new ControllerResponse();
            var dbContext = _dbContextFactory.CreateDbContext();

            dbContext.ChangeTracker.Clear();
            dbContext.Entry(entity).State = EntityState.Modified;
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                response.AppendNewError(e.Message);
                response.AppendNewError($"{nameof(RepoUpdate<TDbContext>)} osztály, {nameof(UpdateAsync)} metódusban hiba keletkezett");
                response.AppendNewError($"{entity} frissítése nem sikerült!");

            }
            return response;
        }
    }
}
