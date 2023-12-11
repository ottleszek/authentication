using LibraryClientServiceTemplate.ModelBrokerConnectors;
using LibraryCore.Errors;
using LibraryCore.Model;
using LibraryCore.Responses;

namespace LibraryBlazorMvvm.ViewModels
{
    public class MvvmCrudViewModelBase<TItem> : MvvmUpdateViewModelBase<TItem>, IMvvmCrudViewModelBase<TItem> where TItem : class, IDbRecord<TItem>, new()
    {
        private readonly ICrudBrokerConnector<TItem>? _broker;

        public MvvmCrudViewModelBase(ICrudBrokerConnector<TItem> broker, IUpdateBrokerConnector<TItem> updateConnector, IGetBrokerConnector<TItem> getBrokerConnector) : base(updateConnector, getBrokerConnector)
        {
            _broker=broker;
        }

        public async Task<ErrorStore> DeleteAsync(Guid id) 
        {
            IsBusy = true;
            ErrorStore errorStore = new ErrorStore();
            if (_broker is null)
            {
                errorStore.ClearAndAddError("Az adatok törlése nem lehetséges!");
            }
            else
            {
                ControllerResponse response = await _broker.DeleteAsync(id);
                if (response.IsSuccess)
                {
                    // reload
                }
                errorStore = (ErrorStore)response;
            }
            IsBusy = false;
            return errorStore;
        }

        public async Task<ErrorStore> InsertAsync(TItem entity)
        {
            IsBusy = true;
            ErrorStore errorStore = new ErrorStore();
            if (_broker is null)
            {
                errorStore.ClearAndAddError("Az adatok mentése nem lehetséges!");
            }
            else
            {
                ControllerResponse response = await _broker.InsertAsync(entity);
                if (response.IsSuccess)
                {
                    // reload
                }
                errorStore = (ErrorStore)response;
            }
            IsBusy = false;
            return errorStore;
        }
    }
}
