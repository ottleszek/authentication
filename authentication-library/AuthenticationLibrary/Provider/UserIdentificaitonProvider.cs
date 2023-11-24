using AuthenticationLibrary.Provider.UserIdentification;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AuthenticationLibrary.Provider
{
    public class UserIdentificaitonProvider : IUserIdentificaitonProvider, IDisposable
    {
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
                UserIdentificationData.UserDisplayedName = UserDisplayNameFrom(claims);
                UserIdentificationData.UserRole = UserRoleFrom(claims);
                UserIdentificationData.Debug=DebugDataFrom(claims);
            }
        }

        private string UserDisplayNameFrom(List<Claim> claims)
        {
            string? firstName = claims.Where(claim => claim.Type == "FirstName").Select(claim => claim.Value).FirstOrDefault();
            string? lastName = claims.Where(claim => claim.Type == "LastName").Select(claim => claim.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                string? email = claims.Where(claim => claim.Type == "Email").Select(claim => claim.Value).FirstOrDefault();
                if (string.IsNullOrEmpty(email))
                {
                    return string.Empty;
                }
                return email;
            }
            return $"{lastName} {firstName}";
        }

        private string UserRoleFrom(List<Claim> claims)
        {
            string? role = claims.Where(claim => claim.Type == "Szerep").Select(claim => claim.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(role))
            {
                return string.Empty;
            }
            return role;
        }

        private string DebugDataFrom(List<Claim> claims)
        {
            string debugData = string.Empty;
            debugData = "<table cellpadding=10>";
            foreach (Claim claim in claims)
            {
                debugData = $"{debugData}<tr>";
                if (debugData.Any())
                    debugData = $"{debugData} <td>({claim.ValueType}</td> <td> {claim.Issuer}</td><td> {claim.Value}</td>";
                else
                    debugData = $"<td>({claim.ValueType} </td><td> {claim.Issuer}</td> <td> {claim.Value}</td>";
                debugData = $"{debugData}</tr>";
            }
            return debugData;
        }         
    }
}
