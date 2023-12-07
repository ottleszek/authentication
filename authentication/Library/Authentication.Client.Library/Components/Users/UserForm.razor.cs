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
        [Parameter] public Guid Id { get; set; } = Guid.Empty;

        private UserValidation? _validation;
        private MudForm _form = new();

        protected async override Task OnParametersSetAsync()
        {
            if (ViewModel is not null)
            {
                ViewModel.Id = Id;
                _validation = new UserValidation();

                await ViewModel.Loading();
            }
            await base.OnParametersSetAsync();
        }

        private async Task SubmitFormAsync()
        {
            //OnInitializedAsync();
        }
    }
}
