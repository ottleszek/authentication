using CommunityToolkit.Mvvm.ComponentModel;
using LibraryClientServiceTemplate.ModelBrokerConnectors;
using LibraryCore.Model;

namespace LibraryBlazorMvvm.ViewModels
{
    public abstract partial class MvvmItemViewModelBase<TItem> : MvvmViewModelBase, IMvvmItemViewModelBase<TItem> where TItem : class, IDbRecord<TItem>, new()
    {
		private IGetBrokerConnector<TItem> _brokerConnector;

        public MvvmItemViewModelBase(IGetBrokerConnector<TItem> brokerConnector)
        {
            _brokerConnector = brokerConnector;
        }

        [ObservableProperty]
        private TItem _selectedItem=new();

		[ObservableProperty]
		private Guid _id = Guid.Empty; 

		public override async Task Loading()
		{
			if (Id != Guid.Empty)
			{
				await GetByIdAsnyc();
			}
			else
			{
				SelectedItem = new();
			}
			
		}

		private async Task GetByIdAsnyc()
		{
			if (Id != Guid.Empty)
			{
				SelectedItem = await _brokerConnector.GetByAsnyc(Id);
			}
		}
	}
}
