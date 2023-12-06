using LibraryCore.Model;

namespace LibraryDataBroker
{
    public interface IListDataBroker : IGetDataBroker
	{
        public Task<List<TEntity>> SelectAllRecordAsync<TEntity>() where TEntity : class, IDbRecord<TEntity>, new();
    }
}
