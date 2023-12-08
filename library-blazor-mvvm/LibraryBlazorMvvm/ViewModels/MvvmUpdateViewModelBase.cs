using LibraryClientServiceTemplate.ModelBrokerConnectors;
using LibraryCore.Errors;
using LibraryCore.Model;
using LibraryCore.Responses;


namespace LibraryBlazorMvvm.ViewModels
{
    public class MvvmUpdateViewModelBase<TItem> : MvvmItemViewModelBase<TItem>, IMVVMUpdateViewModelBase<TItem> where TItem : class, IDbRecord<TItem>, new()
    {
        private readonly IUpdateBrokerConnector<TItem> _brokckerConnector;

        public MvvmUpdateViewModelBase(IUpdateBrokerConnector<TItem> brokerConnector, IGetBrokerConnector<TItem> getBrokerConnector) : 
            base(getBrokerConnector)
        {
            _brokckerConnector = brokerConnector;
        }

        public void ResetData()
        {
            if (_tempItem is not null)
            {
                SelectedItem = (TItem)_tempItem.Clone();
            }
        }

        public async Task<ErrorStore> UpdateAsync()
        {
            IsBusy = true;
            ErrorStore errorStore = new ErrorStore();
            if (_brokckerConnector is null)
            {
                errorStore.ClearAndAddError("Az adatok frissítése nem lehetséges!");
            }
            else
            {
                ControllerResponse response = await _brokckerConnector.UpdateAsync(SelectedItem);
                if (response.IsSuccess)
                {
                    await GetByIdAsnyc();
                    SaveToTempData();
                }
                errorStore = (ErrorStore) response;
            }
            IsBusy = false;
            return errorStore;
        }
    }
}
