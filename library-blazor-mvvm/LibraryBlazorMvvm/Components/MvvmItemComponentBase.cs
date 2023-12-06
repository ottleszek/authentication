using LibraryBlazorMvvm.ViewModels;
using Microsoft.AspNetCore.Components;

namespace LibraryBlazorMvvm.Components
{
    public abstract class MvvmItemComponentBase<TItem,TViewModel> : ComponentBase
        where TViewModel : IMvvmItemViewModelBase<TItem>
        where TItem : class, new()
    {
        [Inject]
        protected TViewModel? ViewModel { get; set; }

        protected override void OnInitialized()
        {
            if (ViewModel is not null)
            {
                ViewModel.PropertyChanged += (_, _) => StateHasChanged();
                base.OnInitialized();
            }
        }

        protected override Task OnInitializedAsync()
        {
            if (ViewModel is not null)
            {
                return ViewModel.OnInitializedAsync();
            }
            else { return Task.CompletedTask; }
        }
    }
}
