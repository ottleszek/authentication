using Authentication.Shared.Models;
using LibraryBlazorMvvm.Components;
using LibraryBlazorMvvm.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Authentication.Client.Library.Components
{ 
    public partial class UserForm : MvvmItemComponentBase<User, MvvmItemViewModelBase<User>>
    {
        [Parameter] public Guid Id { get; set; } = Guid.Empty;

        protected override Task OnParametersSetAsync()
        {
            if (ViewModel is not null)
            {
                ViewModel.Id = Id;
            }
            return base.OnParametersSetAsync();
        }

        private async Task SubmitFormAsync()
        {
            OnInitializedAsync();
        }
    }
}
