using LibraryCore.Model;

namespace LibraryClientServiceTemplate.ModelBrokerConnectors
{
    public interface IIncludedBrokerConnector<TItem> : IListBrokerConnector<TItem>
    {
        public Task<List<TItem>> SelectAllRecordIncludedToListAsync();
    }
}
