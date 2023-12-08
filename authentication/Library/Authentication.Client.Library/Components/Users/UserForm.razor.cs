using Authentication.Client.Library.Validation;
using Authentication.Shared.Models;
using LibraryBlazorMvvm.Components;
using LibraryBlazorMvvm.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Authentication.Client.Library.Components
{ 
    public partial class UserForm : MvvmItemComponentBase<User, MvvmItemViewModelBase<User>>
    {
        private MudForm _form = new();

        [Parameter] public Guid Id { get; set; } = Guid.Empty;
        [Inject] private NavigationManager? Navigation { get; set; }
        [Inject] private UserValidation? Validation { get; set; }              


        protected async override Task OnParametersSetAsync()
        {
            if (ViewModel is not null)
            {
                ViewModel.Id = Id;
                Validation = new UserValidation();

                await ViewModel.Loading();
            }
            await base.OnParametersSetAsync();
        }

        private void GoBack()
        {
            if (Navigation is not null)
                Navigation.NavigateTo("/user");
        }

        private async Task SubmitFormAsync()
        {
            //OnInitializedAsync();
        }
    }
}
