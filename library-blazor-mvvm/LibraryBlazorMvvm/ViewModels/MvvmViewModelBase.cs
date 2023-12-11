using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace LibraryBlazorMvvm.ViewModels
{

	public partial class MvvmViewModelBase : ObservableObject, IMvvmViewModelBase
    {
        [ObservableProperty]
        private bool _isBusy = false;

        public virtual async Task OnInitializedAsync()
        {
            IsBusy = true;
            await Loading().ConfigureAwait(true);
            IsBusy = false;
        }        

        [RelayCommand]
        public virtual async Task Loading()
        {
            IsBusy = true;
            await Task.CompletedTask.ConfigureAwait(false);
            IsBusy=false;
        }

        protected virtual void NotifyStateChanged() => OnPropertyChanged((string?)null);
    }
}
