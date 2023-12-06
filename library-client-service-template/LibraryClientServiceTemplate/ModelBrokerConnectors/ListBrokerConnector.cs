using LibraryCore.Model;
using LibraryDataBroker;

namespace LibraryClientServiceTemplate.ModelBrokerConnectors
{
    public class ListBrokerConnector<TItem> : GetBrokerConnector<TItem>,  IListBrokerConnector<TItem> where TItem : class, IDbRecord<TItem>, new()
    {
        private readonly IListDataBroker _broker;

        public ListBrokerConnector(IListDataBroker broker, IGetDataBroker getDataBroker)
            : base(getDataBroker)
        {
            _broker = broker;  
        }
		public async Task<List<TItem>> SelectAllRecordAsync()
		{
            return await _broker.SelectAllRecordAsync<TItem>();
		}
	}
}
