using LibraryCore.Errors;
using LibraryCore.Model;

namespace LibraryBlazorMvvm.ViewModels
{
    public interface IMvvmCrudViewModelBase<TItem> : IMVVMUpdateViewModelBase<TItem>
    {
        public Task<ErrorStore> InsertAsync<TEntity>(TEntity entity) where TEntity : class, IDbRecord<TEntity>, new();
        public Task<ErrorStore> DeleteAsync<TEntity>(TEntity entity) where TEntity : class, IDbRecord<TEntity>, new();
    }
}
