using Authentication.Shared.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Authentication.Server.Services
{
    public interface ITokenService
    {
        public string GenerateJwtToken(User user);
        public Task<string> GenerateJwtToken(User user, IdentityUser identityUser, string userEnglishRoleName);
        public string GenerateRefreshToken();
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
