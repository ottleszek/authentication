using LibraryClientServiceTemplate.ModelBrokerConnectors;
using LibraryCore.Model;
using LibraryCore.Responses;

namespace LibraryClientServiceTemplate.ViewModelsTemplate
{
    public class ListAndDeleteViewModel<TItem> : ListViewModel<TItem>, IListAndDeleteViewModel where TItem : class, IDbRecord<TItem>, new()
    {
        public IListAndDeleteBrokerConnector<TItem> _service;

        public ListAndDeleteViewModel(IListAndDeleteBrokerConnector<TItem> service, IListBrokerConnector<TItem> listService) : base(listService)
        {
            _service = service;
        }

        public Task<ControllerResponse> DeleteAsync(Guid id)
        {
            return _service.DeleteAsync(id);
        }
    }
}
