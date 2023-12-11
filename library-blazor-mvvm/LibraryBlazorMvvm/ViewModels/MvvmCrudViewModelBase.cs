using LibraryClientServiceTemplate.ModelBrokerConnectors;
using LibraryCore.Errors;
using LibraryCore.Model;

namespace LibraryBlazorMvvm.ViewModels
{
    public class MvvmCrudViewModelBase<TItem> : MvvmUpdateViewModelBase<TItem>, IMvvmCrudViewModelBase<TItem> where TItem : class, IDbRecord<TItem>, new()
    {
        public MvvmCrudViewModelBase(IUpdateBrokerConnector<TItem> brokerConnector, IGetBrokerConnector<TItem> getBrokerConnector) : base(brokerConnector, getBrokerConnector)
        {
        }

        Task<ErrorStore> DeleteAsync<TEntity>(TEntity entity)
        {

        }

        Task<ErrorStore> InsertAsync<TEntity>(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
