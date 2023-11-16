using AuthenticationLibrary.Services.Token;
using AuthenticationLibrary.Shared.Dtos;
using LibraryCore.Errors;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace AuthenticationLibrary.Services.Accounts
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILoginTokenStore? _accessAndRefreshTokenService;
        private readonly INofifyAuthenticationService _nofifyAuthenticationService;


        public AuthenticationService(IHttpClientFactory httpClientFactory, ILoginTokenStore loginTokenStore, INofifyAuthenticationService nofifyAuthenticationService)
        {
            _httpClient = httpClientFactory.CreateClient("AuthenticationApi");
            _accessAndRefreshTokenService = loginTokenStore;
            _nofifyAuthenticationService = nofifyAuthenticationService;                
        }

        public async Task<ErrorStore> LoginAsync(UserLoginDto loginPlayload)
        {
            TokenResponseDto tokenResponseDto = await CallLoginApi(loginPlayload);
            if (!tokenResponseDto.IsLoggedIn)
            {
                return tokenResponseDto;
            }
            else
            {
                ErrorStore saveLoginDataError = await SaveLoginData(tokenResponseDto);
                if (saveLoginDataError.HasError)
                    return saveLoginDataError;
                else
                {
                    NotifyUserAuthentication(tokenResponseDto);                    
                }
            }
            return new ErrorStore();
        }

        public async Task Logout()
        {
            if (_accessAndRefreshTokenService is not null)
            {
                ErrorStore error = await _accessAndRefreshTokenService.DeleteAccessTokenAndRefreshToken();
                _nofifyAuthenticationService.NotifyLogOut();
            }

        }

        private async Task<TokenResponseDto> CallLoginApi(UserLoginDto loginPlayload)
        {
            TokenResponseDto? loginResponse = new();
            if (_httpClient is null)
            {
                loginResponse.ClearAndAddError("Bejelentkezés nem lehetséges!");
            }
            else
            {
                try
                {
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Login/login", loginPlayload);

                    string content = await response.Content.ReadAsStringAsync();
                    loginResponse = JsonConvert.DeserializeObject<TokenResponseDto>(content);
                    if (loginResponse is null)
                    {
                        loginResponse = new TokenResponseDto();
                        loginResponse.ClearAndAddError("A felhasználói adatok elérése nem lehetséges!");
                    }

                }
                catch (Exception ex)
                {
                    LibraryLogging.LoggingBroker.LogError($"{ex.Message}");
                    if (loginResponse is null)
                        loginResponse = new TokenResponseDto();
                    loginResponse.ClearAndAddError("Hiba történt a művelet közben! A felhasználói adatok elérése nem lehetséges!");
                    return loginResponse;
                }
            }
            return loginResponse;
        }

        private async Task<ErrorStore> SaveLoginData(TokenResponseDto loginResponse)
        {
            ErrorStore errorStore = new ErrorStore();
            if ( _accessAndRefreshTokenService is null || !loginResponse.HasAccessToken || !loginResponse.HasRefreshToken)
            {
                errorStore.ClearAndAddError("Bejelentkezési adatok tárolása nem lehetséges!");
            }
            else
            {
                errorStore = await _accessAndRefreshTokenService.SaveAccessTokenAndRefreshToken(loginResponse.AccessToken, loginResponse.RefreshToken);
                return errorStore;
            }
            return loginResponse;
        }

        private void NotifyUserAuthentication(TokenResponseDto loginResponse)
        {
            if (_nofifyAuthenticationService is not null && loginResponse.HasAccessToken )
            {
                _nofifyAuthenticationService.NotifyUseAuthentication(loginResponse.AccessToken);
            }
        }
    }
}
