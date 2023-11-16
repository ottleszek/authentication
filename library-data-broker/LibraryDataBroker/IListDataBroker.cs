using LibraryCore.Model;

namespace LibraryDataBrokerProject
{
    public interface IListDataBroker
    {
        public Task<List<TEntity>> SelectAllRecordAsync<TEntity>() where TEntity : class, IDbRecord<TEntity>, new();
        public Task<TEntity> GetBy<TEntity>(Guid id) where TEntity : class, IDbRecord<TEntity>, new();

    }
}
