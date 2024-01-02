using LibraryCore.Model;
using LibraryDataBroker;

namespace LibraryClientServiceTemplate.ModelBrokerConnectors
{
    public class InludedBrokerConnector<TItem> : ListBrokerConnector<TItem>, IIncludedBrokerConnector<TItem> where TItem : class, IDbRecord<TItem>, new()
    {
        private readonly IIncludedDataBroker _includedDataBroker;

        public InludedBrokerConnector(IIncludedDataBroker includedBroker, IListDataBroker broker, IGetDataBroker getDataBroker) : base(broker, getDataBroker)
        {
            _includedDataBroker = includedBroker;
        }

        public async Task<List<TItem>> SelectAllRecordIncludedToListAsync()
        {
            return await _includedDataBroker.SelectAllRecordIncludedToListAsync<TItem>();
        }
    }
}
