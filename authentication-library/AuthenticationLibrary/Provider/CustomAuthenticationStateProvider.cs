using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace AuthenticationLibrary.Provider
{
    public enum AuthenticationStateType { LoggedIn, LoggedOut }

    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private ILocalStorageService _localStorage;
        private readonly AuthenticationState _anonymous;

        public CustomAuthenticationStateProvider(HttpClient client, ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
            _httpClient = client;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public EventHandler? AuthenticationChanged { get; set; }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("jwt-access-token");
            if (string.IsNullOrWhiteSpace(token))
                return _anonymous;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "bearer")));
        }

        public void NotifyUserAuthentication(string token)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "bearer"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));

            NotifyAuthenticationStateChanged(authState);
            OnAuthenticationChanged(AuthenticationStateType.LoggedIn);
        }
        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(_anonymous);
            NotifyAuthenticationStateChanged(authState);
            OnAuthenticationChanged(AuthenticationStateType.LoggedOut);
        }

        private void OnAuthenticationChanged(AuthenticationStateType authenticationState)
        {
            AuthenticationChanged?.Invoke(this, new EventArgs());
        }
    }
}
