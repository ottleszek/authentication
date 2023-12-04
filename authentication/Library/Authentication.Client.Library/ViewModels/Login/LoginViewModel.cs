using Authentication.Shared.Dtos;
using Authentication.Shared.Services.Accounts;
using CommunityToolkit.Mvvm.ComponentModel;
using LibraryCore.Errors;
using LibraryBlazorMvvm.Base;


namespace Authentication.Client.Library.ViewModels.Login
{
    public partial class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticationService? _authenticationService;

        [ObservableProperty]
        private string _email=string.Empty;

        [ObservableProperty]
        private string _password=string.Empty;

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
                    UserLoginDto userLoginDto = new UserLoginDto
                    {
                        Email = Email,
                        Password=Password
                    };
                    ErrorString = await _authenticationService.LoginAsync(userLoginDto);
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
