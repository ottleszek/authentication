using CommunityToolkit.Mvvm.ComponentModel;
using LibraryClientServiceTemplate.ModelBrokerConnectors;
using LibraryCore.Model;
using LibraryDataBroker;

namespace LibraryBlazorMvvm.ViewModels
{
    public partial class MvvmItemViewModelBase<TItem> : MvvmViewModelBase, IMvvmItemViewModelBase<TItem> where TItem : class, IDbRecord<TItem>, new()
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

        private async Task GetByEntityAsnyc(TItem entity)
		{
			if (entity.Id != Guid.Empty)
			{
				SelectedItem = await _brokerConnector.GetByAsnyc(entity);
			}
		}
	}
}
