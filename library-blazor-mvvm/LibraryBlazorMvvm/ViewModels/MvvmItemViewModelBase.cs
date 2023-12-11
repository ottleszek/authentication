using CommunityToolkit.Mvvm.ComponentModel;
using LibraryClientServiceTemplate.ModelBrokerConnectors;
using LibraryCore.Errors;
using LibraryCore.Model;

namespace LibraryBlazorMvvm.ViewModels
{
    public partial class MvvmItemViewModelBase<TItem> : MvvmViewModelBase, IMvvmItemViewModelBase<TItem> where TItem : class, IDbRecord<TItem>, new()
    {
		private IGetBrokerConnector<TItem> _brokerConnector;
		
		protected TItem? _tempItem = null;

        public MvvmItemViewModelBase(IGetBrokerConnector<TItem> brokerConnector)
        {
            _brokerConnector = brokerConnector;
        }

        [ObservableProperty]
        private Guid _id = Guid.Empty;

        [ObservableProperty]
        private TItem _selectedItem=new();

        [ObservableProperty]
        private ErrorStore _errorStore = new();

        public override async Task Loading()
		{
			IsBusy = true;
			if (Id != Guid.Empty)
			{
				await GetByIdAsnyc();
			}
			else
			{
				SelectedItem = new();
			}
			IsBusy = false;
			
		}

        protected async Task GetByIdAsnyc()
		{
			IsBusy = true;
			if (Id != Guid.Empty)
			{
				SelectedItem = await _brokerConnector.GetByAsnyc(Id);
				SaveToTempData();
            }
			IsBusy = false;
		}

		protected void SaveToTempData()
		{
            _tempItem = (TItem) SelectedItem.Clone();
        }
    }
}
