using LibraryCore.Model;
using LibraryDataBroker;

namespace LibraryDataBrokerProject
{
    public interface IListDataBroker : IGetDataBroker
	{
        public Task<List<TEntity>> SelectAllRecordAsync<TEntity>() where TEntity : class, IDbRecord<TEntity>, new();


    }
}
