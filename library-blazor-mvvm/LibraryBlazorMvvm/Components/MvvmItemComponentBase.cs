using LibraryBlazorMvvm.ViewModels;
using LibraryCore.Model;
using Microsoft.AspNetCore.Components;

namespace LibraryBlazorMvvm.Components
{
    public class MvvmItemComponentBase<TItem,TViewModel> : ComponentBase
        where TItem : class, IDbRecord<TItem>, new()
        where TViewModel : IMvvmItemViewModelBase<TItem>

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
            return Task.CompletedTask;
        }
    }

    
}
