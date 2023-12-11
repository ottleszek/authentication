using LibraryCore.Errors;
using LibraryCore.Model;

namespace LibraryBlazorMvvm.ViewModels
{
    public interface IMvvmCrudViewModelBase<TItem> : IMVVMUpdateViewModelBase<TItem>
    {
        public Task<ErrorStore> InsertAsync(TItem entity);
        public Task<ErrorStore> DeleteAsync(Guid id); 
    }
}
