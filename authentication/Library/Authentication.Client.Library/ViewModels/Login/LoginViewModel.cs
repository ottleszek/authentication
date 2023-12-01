using Authentication.Shared.Dtos;
using Authentication.Shared.Services.Accounts;
using CommunityToolkit.Mvvm.ComponentModel;
using LibraryCore.Errors;
using LibraryMvvm.Base;


namespace Authentication.Client.Library.ViewModels.Login
{
    public partial class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticationService? _authenticationService;

        //        [ObservableProperty]
        //        private EmailValidation? validation = new();

        //[ObservableProperty]
        //private UserLoginDto _user = new();

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private ErrorStore errorString = new();

        public LoginViewModel(IAuthenticationService? service)
        {
            _authenticationService = service;
        }

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
