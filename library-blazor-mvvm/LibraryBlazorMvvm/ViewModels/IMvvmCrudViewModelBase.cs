using LibraryCore.Errors;
using LibraryCore.Model;

namespace LibraryBlazorMvvm.ViewModels
{
    public interface IMvvmCrudViewModelBase<TItem> : IMVVMUpdateViewModelBase<TItem>
    {
        public Task InsertAsync();
        public Task DeleteItemAsync(); 
    }
}
