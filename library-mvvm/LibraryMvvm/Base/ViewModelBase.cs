using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace LibraryMvvm.Base
{
    public abstract partial class ViewModelBase : ObservableObject, IViewModelBase
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
