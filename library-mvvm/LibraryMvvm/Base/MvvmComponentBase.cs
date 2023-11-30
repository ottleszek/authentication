using Microsoft.AspNetCore.Components;

namespace LibraryMvvm.Base
{
    public abstract class MvvmComponentBase<TViewModel> : ComponentBase where TViewModel : IViewModelBase
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
