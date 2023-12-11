using LibraryClientServiceTemplate.ModelBrokerConnectors;
using LibraryCore.Model;

namespace LibraryClientServiceTemplate.ViewModelsTemplate
{
    public class ListViewModel<TItem> : BaseViewModel<TItem>, IListViewModel<TItem> where TItem: class, IDbRecord<TItem>, new ()
    {
        private readonly IListBrokerConnector<TItem> _service;

        public ListViewModel(IListBrokerConnector<TItem> service)
        {
            _service = service;
        }

        public List<TItem>? Items { get; set; }
        public bool HasItems => Items is not null && Items.Any();

        public virtual async Task GetAllDataToViewModel()
        {
            Items = await _service.SelectAllRecordAsync();
        }

        public async Task<List<TItem>> ReloadDataAsync()
        {
            return await _service.SelectAllRecordAsync();
        }
    }
}
