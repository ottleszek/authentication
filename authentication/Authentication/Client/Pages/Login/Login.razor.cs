using AuthenticationLibrary.Services.Accounts;
using AuthenticationLibrary.Shared.Dtos;
using AuthenticationLibrary.ViewModels.Login;
using LibraryCore.Errors;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace Authentication.Client.Pages.Login
{
    public partial class Login
    {
        [CascadingParameter] public Task<AuthenticationState>? AuthState { get; set; }
        [Inject] ILoginViewModel? LoginViewModel { get; set; }
        [Inject] IAuthenticationService? AuthenticationService { get; set; }

        [Inject] NavigationManager? NavigationManager { get; set; }

        private LoginValidation? validation;

        private ErrorStore ErrorString = new();
        private MudForm _form = new();

        protected override Task OnParametersSetAsync()
        {
            if (validation == null)
                validation = new LoginValidation();
            return base.OnParametersSetAsync();
        }

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
            if (LoginViewModel is not null && AuthenticationService is not null && _form.IsValid)
            {
                UserLoginDto userLoginDto = LoginViewModel.ConvertToUserLoginDto;

                ErrorStore logingErrorStore = new();
                try
                {
                    logingErrorStore = await AuthenticationService.LoginAsync(userLoginDto);

                    if (logingErrorStore.HasError)
                    {
                        ErrorString.Error = logingErrorStore.Error;
                    }
                    else
                    {
                        if (NavigationManager is object)
                            NavigationManager.NavigateTo("/");
                    }
                }
                catch (Exception ex)
                {
                    LibraryLogging.LoggingBroker.LogError(ex.Message);
                    ErrorString.ClearAndAddError("Bejelentkezés nem lehetséges");
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
