using Authentication.Client.Library.Validation;
using Authentication.Shared.Dtos;
using Authentication.Shared.Services.Accounts;
using CommunityToolkit.Mvvm.ComponentModel;
using LibraryCore.Errors;
using LibraryMvvm.Base;
using Microsoft.AspNetCore.Components;

namespace Authentication.Client.Library.ViewModels.Login
{
    public partial class LoginViewModel : ViewModelBase
    {
        [Inject] private IAuthenticationService? _authenticationService { get; set; }

        [ObservableProperty]
        private EmailValidation? validation = new();

        [ObservableProperty]
        private UserLoginDto _user = new();

        [ObservableProperty]
        private ErrorStore errorString = new();        

        public async Task LoginAsync()
        {
            try
            {
                if (_authenticationService is not null)
                {
                    ErrorString = await _authenticationService.LoginAsync(User);
                    return;
                }

            }
            catch (Exception ex)
            {
                LibraryLogging.LoggingBroker.LogError(ex.Message);
                
            }
            ErrorString.ClearAndAddError("Bejelentkezés nem lehetséges");
        }
    }
}
