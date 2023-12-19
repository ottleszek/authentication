using LibraryCore.Model;
using LibraryCore.Responses;
using LibraryDataBroker;

namespace LibraryClientServiceTemplate.ModelBrokerConnectors
{
    public class ListAndDeleteBrokerConnector<TItem> : ListBrokerConnector<TItem>, IListAndDeleteBrokerConnector<TItem> where TItem : class, IDbRecord<TItem>, new()
    {
        private readonly ICrudDataBroker _crudBroker;

        public ListAndDeleteBrokerConnector(ICrudDataBroker crudBroker, IListDataBroker broker, IGetDataBroker getDataBroker) : base(broker, getDataBroker)
        {
            _crudBroker = crudBroker;
        }

        public async Task<ControllerResponse> DeleteAsync(Guid id)
        {
            return await _crudBroker.DeleteAsync<TItem>(id);
        }
    }
}
