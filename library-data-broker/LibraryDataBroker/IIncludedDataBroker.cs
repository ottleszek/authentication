using LibraryCore.Model;

namespace LibraryDataBroker
{
    public interface IIncludedDataBroker
    {
        public Task<List<TEntity>> SelectAllRecordIncludedAsync<TEntity>() where TEntity : class, IDbRecord<TEntity>, new();
    }
}
