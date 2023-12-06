using LibraryCore.Model;

namespace LibraryDataBrokerProject
{
    public interface IListDataBroker 
	{
        public Task<List<TEntity>> SelectAllRecordAsync<TEntity>() where TEntity : class, IDbRecord<TEntity>, new();
    }
}
