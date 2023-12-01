using Authentication.Client.Library.Validation;
using Authentication.Client.Library.ViewModels.Login;
using LibraryMvvm.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace Authentication.Client.Library.Components
{
    public partial class LoginComponent : MvvmComponentBase<LoginViewModel>
    {
        [CascadingParameter] public Task<AuthenticationState>? AuthState { get; set; }
        [Inject] NavigationManager? NavigationManager { get; set; }

        private EmailValidation? _validation = new();
        private MudForm _form = new();

        protected override async Task OnInitializedAsync()
        {
            if (AuthState is not null && NavigationManager is not null)
            {
                var user = (await AuthState).User;
                if (user is not null && user.Identity is not null)
                {
                    if (user.Identity.IsAuthenticated)
                        NavigationManager.NavigateTo("/");
                }
            }
            await base.OnInitializedAsync();
        }

        private async Task LoginAsync()
        {
            await _form.Validate();
            if (_form.IsValid && ViewModel is not null)
            {
                await ViewModel.LoginAsync();
                if (! ViewModel.ErrorString.HasError)
                {
                    if (NavigationManager is object)
                        NavigationManager.NavigateTo("/");
                }
            }
        }

        private void GoToRegister()
        {
            if (NavigationManager is not null)
            {
                NavigationManager.NavigateTo("/registration");
            }

        }
    }
}
