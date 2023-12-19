using LibraryCore.Errors;

namespace LibraryBlazorMvvm.ViewModels
{
    public interface IMVVMUpdateViewModelBase<TItem> : IMvvmItemViewModelBase<TItem>
    {
        public void ResetData();
        public Task<ErrorStore> UpdateAsync();

    }
}
