using AuthenticationLibrary.Provider;
using AuthenticationLibrary.Provider.UserIdentification;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Data;
using System.Security.Claims;

namespace Authentication.Client.Library.Components
{
    public class UserIdentificationBase : ComponentBase
    {
        // Kiolvassa a felhasználói adatokat

        [Inject] private AuthenticationStateProvider? AuthenticationStateProvider { get; set; }
        //[Inject] private IUserIdentificaitonProvider? UserIdentificationProvider { get; set; }

        protected UserIdentificationData? userIdentificationData=null;

        protected override async Task OnInitializedAsync()
        {
            /*if (AuthenticationStateProvider is not null)
            {
                AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                ClaimsPrincipal user = authState.User;
                if (user.Identity is not null && user.Identity.IsAuthenticated)
                {
                    userIdentificationData = new();
                    userIdentificationData.IsAuthenticated = true;
                }
                else
                {
                    userIdentificationData = new();
                    userIdentificationData.IsAuthenticated = true;
                }
            }*/
            await base.OnInitializedAsync();
        }

        protected async Task<string> GetUserEmail()
        {
            List<Claim>? claims = await GetClaims();
            if (claims is not null)
                return UserIdentificationDataExtensions.GetUserEmail(claims) ;
            return string.Empty;
        }

        protected async Task<string> GetUserDisplayName()
        {
            List<Claim>? claims = await GetClaims();
            if (claims is not null)
                return UserIdentificationDataExtensions.GetUserDisplayNameFrom(claims);
            return string.Empty;
        }

        protected async Task<string> GetUserRole()
        {
            List<Claim>? claims = await GetClaims();
            if (claims is not null)
            {
                return UserIdentificationDataExtensions.GetUserRoleFrom(claims);
            }
            return string.Empty;
        }

        protected async Task<string> GetUserDebugData()
        {
            List<Claim>? claims = await GetClaims();
            if (claims is not null)
            {
               return UserIdentificationDataExtensions.GetDebugDataFrom(claims);
            }
            return string.Empty;
        }

        private async Task<List<Claim>?> GetClaims()
        {
            if (AuthenticationStateProvider is not null)
            {
                AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                ClaimsPrincipal user = authState.User;
                if (user.Identity is not null && user.Identity.IsAuthenticated)
                {
                    userIdentificationData = new();
                    userIdentificationData.IsAuthenticated = true;
                    return user.Claims.ToList();
                }
            }
            return null;
        }
    }
}
