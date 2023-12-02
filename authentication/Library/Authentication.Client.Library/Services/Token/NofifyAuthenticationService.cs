using Microsoft.AspNetCore.Components.Authorization;
using AuthenticationLibrary.Provider;

namespace Authentication.Shared.Services.Token
{
    public class NofifyAuthenticationService : INofifyAuthenticationService
    {
        private readonly AuthenticationStateProvider? _authenticationStateProvider;

        public NofifyAuthenticationService(AuthenticationStateProvider authStateProvider)
        {
            _authenticationStateProvider=authStateProvider;
        }

        public void NotifyUseAuthentication(string accessJwtToken)
        {
            if (_authenticationStateProvider is not null)
            {
                ((CustomAuthenticationStateProvider) _authenticationStateProvider).NotifyUserAuthentication(accessJwtToken);
            }
        }

        public void NotifyLogOut()
        {
            if (_authenticationStateProvider is not null)
            {
                ((CustomAuthenticationStateProvider)_authenticationStateProvider).NotifyUserLogout();
            }                    
        }
    }
}
