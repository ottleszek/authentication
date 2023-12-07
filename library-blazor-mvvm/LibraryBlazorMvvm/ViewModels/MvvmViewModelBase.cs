using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace LibraryBlazorMvvm.ViewModels
{

	public partial class MvvmUserViewModelBase : IMvvmUserViewModelBase
	{

        public MvvmUserViewModelBase()
        {                
        }
	}


	public partial class MvvmViewModelBase : ObservableObject, IMvvmViewModelBase
    {
        public virtual async Task OnInitializedAsync()
        {
            await Loading().ConfigureAwait(true);
        }

        protected virtual void NotifyStateChanged() => OnPropertyChanged((string?)null);

        [RelayCommand]
        public virtual async Task Loading()
        {
            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}
