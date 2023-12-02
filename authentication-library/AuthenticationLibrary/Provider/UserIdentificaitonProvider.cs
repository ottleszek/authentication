using AuthenticationLibrary.Provider.UserIdentification;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AuthenticationLibrary.Provider
{
    public class UserIdentificaitonProvider : IUserIdentificaitonProvider, IDisposable
    {
        // Figyeli a felhasználói adatok változását
        // Fel lehet iratkozni a UserIdentificationDataChanged eseményre
        // Ha bejeletkezés után AuthenticationStateChanged történik akkor meghívja az eseményt

        private CustomAuthenticationStateProvider? _authenticationStateProvider;
        
        public UserIdentificaitonProvider(AuthenticationStateProvider? stateProvider)
        {
            if (stateProvider is not null)
            {
                _authenticationStateProvider = (CustomAuthenticationStateProvider)stateProvider;
                stateProvider.AuthenticationStateChanged += OnAuthenticationStateChanged;
            }
        }

        public event EventHandler? UserIdentificationDataChanged;

        public UserIdentificationData? UserIdentificationData { get; set; } = null;
        public bool IsLoaded => UserIdentificationData is not null;

        public async Task<UserIdentificationData?> GetUserIdentificationData()
        {
            UserIdentificationData = null;
            if (_authenticationStateProvider is not null)
            {
                AuthenticationState authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                ClaimsPrincipal user = authState.User;
                
                if (user.Identity is not null && user.Identity.IsAuthenticated)
                {
                    if (UserIdentificationData is null)
                    {
                        UserIdentificationData = new UserIdentificationData();
                    }
                    UserIdentificationData.IsAuthenticated = true;
                    RefreshUserIdentificationDataFrom(user.Claims.ToList());
                }
                else
                {
                    if (UserIdentificationData is null)
                    {
                        UserIdentificationData = new UserIdentificationData();
                    }
                    UserIdentificationData = new UserIdentificationData();
                    UserIdentificationData.IsAuthenticated = false;
                }
            }
            return UserIdentificationData;
        }

        public void Dispose()
        {
            if (_authenticationStateProvider is not null)
            {
                _authenticationStateProvider.AuthenticationStateChanged -= OnAuthenticationStateChanged;
            }
        }

        private void OnAuthenticationStateChanged(Task<AuthenticationState> task)
        {
            if (UserIdentificationDataChanged is not null)
            {
                UserIdentificationDataChanged.Invoke(this, new EventArgs());
            }
        }

        private void RefreshUserIdentificationDataFrom(List<Claim> claims)
        {
            if (UserIdentificationData is not null)
            {
                UserIdentificationData.UserDisplayedName = UserIdentificationDataExtensions.GetUserDisplayNameFrom(claims);
                UserIdentificationData.UserRole = UserIdentificationDataExtensions.GetUserRoleFrom(claims);
                UserIdentificationData.Debug= UserIdentificationDataExtensions.GetDebugDataFrom(claims);
            }
        }
    }
}
