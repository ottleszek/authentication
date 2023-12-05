using CommunityToolkit.Mvvm.ComponentModel;
using LibraryClientServiceTemplate.ModelBrokerConnectors;
using LibraryDataBroker;

namespace LibraryBlazorMvvm.ViewModels
{
    public partial class MvvmItemViewModelBase<TItem> : MvvmViewModelBase, IGetDataBroker  where TItem : class
    {
		private IGetBrokerConnector<TItem> _brokerConnector;

		ctor

		[ObservableProperty]
        public TItem? _selectedItem;



		public Task<TEntity> GetByAsnyc<TEntity>(Guid id)
		{
			if (id!=Guid.Empty)
			{

			}
		}

		public Task<TEntity> IGetDataBroker.GetByAsnyc<TEntity>(TEntity entity)
		{
            if (entity.Id!=Guid.Empty)
            {
                
            }
        }
	}
}
