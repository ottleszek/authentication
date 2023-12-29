using LibraryClientServiceTemplate.ModelBrokerConnectors;
using LibraryCore.Model;

namespace LibraryClientServiceTemplate.ViewModelsTemplate
{
    public class IncludedListViewModel<TItem> : ListViewModel<TItem>, IIncludedListViewModel<TItem> where TItem : class, IDbRecord<TItem>, new()
    {
        private readonly IIncludedBrokerConnector<TItem> _includedConnector;

        public IncludedListViewModel(IIncludedBrokerConnector<TItem> includedBrokerConnector, IListBrokerConnector<TItem> service) : base(service)
        {
            _includedConnector = includedBrokerConnector;
        }

        public async Task GetAllDataIncludedToViewModelAsync()
        {
            Items = await _includedConnector.SelectAllRecordIncludedToListAsync();
        }

        public async Task<List<TItem>> ReloadIncludedDataAsync()
        {
            return await _includedConnector.SelectAllRecordIncludedToListAsync();
        }
    }
}
