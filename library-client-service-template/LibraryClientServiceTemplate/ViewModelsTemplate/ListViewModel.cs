using LibraryClientServiceTemplate.ModelBrokerConnectors;
using LibraryCore.Model;

namespace LibraryClientServiceTemplate.ViewModelsTemplate
{
    public class ListViewModel<TItem> : BaseViewModel<TItem>, IListViewModel<TItem> where TItem: class, IDbRecord<TItem>, new ()
    {
        private readonly IListModelBrokerConnector<TItem> _service;

        public ListViewModel(IListModelBrokerConnector<TItem> service)
        {
            _service = service;
        }

        public List<TItem>? Items { get; set; }
        public bool HasItems => Items is not null && Items.Any();

        public virtual async Task GetAllDataToViewModel()
        {
            Items = await _service.SelectAllRecordAsync<TItem>();
        }

        public async Task<List<TItem>> ReloadDataAsync()
        {
            return await _service.SelectAllRecordAsync<TItem>();
        }
    }
}
