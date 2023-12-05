using CommunityToolkit.Mvvm.ComponentModel;

namespace LibraryBlazorMvvm.ViewModels
{
    public partial class MvvmItemViewModelBase<TItem> : MvvmViewModelBase  where TItem : class
    {
        [ObservableProperty]
        public TItem? _selectedItem;
    }
}
