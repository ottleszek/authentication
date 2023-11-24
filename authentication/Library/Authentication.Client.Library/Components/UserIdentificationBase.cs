using Authentication.Shared.Models;
using AuthenticationLibrary.Provider.UserIdentification;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Data;
using System.Security.Claims;

namespace Authentication.Client.Library.Components
{
    public class UserIdentificationBase : ComponentBase
    {
        [Inject] private AuthenticationStateProvider? AuthenticationStateProvider { get; set; }

        protected UserIdentificationData? userIdentificationData=null;

        protected override async Task OnInitializedAsync()
        {
            if (AuthenticationStateProvider is not null)
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
            }
            await base.OnInitializedAsync();
        }

        protected async Task<string> GetUserEmail()
        {
            List<Claim>? claims = await GetClaims();
            if (claims is not null)
            {
                string? email = claims.Where(claim => claim.Type == "Email").Select(claim => claim.Value).FirstOrDefault();
                if (!string.IsNullOrEmpty(email))
                {
                    return email;
                }
            }
            return string.Empty;
        }

        protected async Task<string> GetUserDisplayName()
        {
            List<Claim>? claims = await GetClaims();
            if (claims is not null)
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
            return string.Empty;
        }

        protected async Task<string> GetUserRole()
        {
            List<Claim>? claims = await GetClaims();
            if (claims is not null)
            {
                string? role = claims.Where(claim => claim.Type == "Szerep").Select(claim => claim.Value).FirstOrDefault();
                if (string.IsNullOrEmpty(role))
                {
                    return string.Empty;
                }
                return role;
            }
            return string.Empty;
        }

        protected async Task<string> GetUserDebugData()
        {
            List<Claim>? claims = await GetClaims();
            if (claims is not null)
            {
                string debugData = string.Empty;
                debugData = "<table>";
                foreach (Claim claim in claims)
                {
                    debugData = $"{debugData}<tr>";
                    if (debugData.Any())
                        debugData = $"{debugData}<td>Type:{claim.Type}</td> <td> {claim.Issuer}</td><td>Value: {claim.Value}</td>";
                    else
                        debugData = $"<td>Type: {claim.Type} </td><td> {claim.Issuer}</td> <td>Value: {claim.Value}</td>";
                    debugData = $"{debugData}</tr>";
                }
                return debugData;
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
