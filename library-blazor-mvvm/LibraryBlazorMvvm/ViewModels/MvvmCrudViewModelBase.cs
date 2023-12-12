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

        public async Task InsertAsync()
        {
            await InsertAsync(SelectedItem);
        }

        public async Task DeleteAsync()
        {
                await DeleteAsync(SelectedItem.Id);
        }

        private async Task<ErrorStore> InsertAsync(TItem entity)
        {
            IsBusy = true;
            if (_broker is null)
            {
                ErrorStore.ClearAndAddError("Az adatok mentése nem lehetséges!");
            }
            else
            {
                ControllerResponse response = await _broker.InsertAsync(entity);
                if (response.IsSuccess)
                {
                    // reload
                }
                ErrorStore = (ErrorStore)response;
            }
            IsBusy = false;
            return ErrorStore;
        }

        private async Task<ErrorStore> DeleteAsync(Guid id) 
        {
            IsBusy = true;
            if (_broker is null)
            {
                ErrorStore.ClearAndAddError("Az adatok törlése nem lehetséges!");
            }
            else
            {
                ControllerResponse response = await _broker.DeleteAsync(id);
                if (response.IsSuccess)
                {
                    // reload
                }
                ErrorStore = (ErrorStore)response;
            }
            IsBusy = false;
            return ErrorStore;
        }       
    }
}
