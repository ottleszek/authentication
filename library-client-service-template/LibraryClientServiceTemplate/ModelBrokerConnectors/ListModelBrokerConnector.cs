using LibraryCore.Model;
using LibraryDataBrokerProject;

namespace LibraryClientServiceTemplate.ModelBrokerConnectors
{
    public class ListModelBrokerConnector<TItem> : IListModelBrokerConnector<TItem> where TItem : class, IDbRecord<TItem>, new()
    {
        private readonly IListDataBroker _listDataBroker;

        public ListModelBrokerConnector(IListDataBroker listDataBroker)
        {
            _listDataBroker = listDataBroker;
        }

        public async Task<TEntity> GetBy<TEntity>(Guid id) where TEntity : class, IDbRecord<TEntity>, new()
        {
            return await _listDataBroker.GetBy<TEntity>(id);
        }

        public async Task<List<TEntity>> SelectAllRecordAsync<TEntity>() where TEntity : class, IDbRecord<TEntity>, new()
        {
            return await _listDataBroker.SelectAllRecordAsync<TEntity>();
        }
    }
}
