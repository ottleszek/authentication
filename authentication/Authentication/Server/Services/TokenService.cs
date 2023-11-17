using Authentication.Shared.Models;
using AuthenticationLibrary.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Authentication.Server.Services
{
    public class TokenService : ITokenService
    {
        private TokenSettings? _tokenSettings;
        private readonly UserManager<IdentityUser>? _userManager;

        public TokenService(IConfiguration? configuration, UserManager<IdentityUser> userManager)
        {
            if (configuration is not null)
                _tokenSettings = configuration.GetSection(nameof(TokenSettings)).Get<TokenSettings>();
            else
                _tokenSettings = new TokenSettings();
            _userManager = userManager;
        }

        public string GenerateJwtToken(User user)
        {
            JwtSecurityToken securityToken = GenerateTokenOptions(GetSigningCredentials(), GetClaims(user));
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public async Task<string> GenerateJwtToken(User user, IdentityUser identityUser, string userEnglishRoleName)
        {
            List<Claim> cliams = await GetClaims(user, identityUser, userEnglishRoleName);

            JwtSecurityToken securityToken = GenerateTokenOptions(GetSigningCredentials(), cliams);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {

            if (_tokenSettings is null)
                _tokenSettings = new TokenSettings();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecretKey)),
                ValidateLifetime = false,
                ValidIssuer = _tokenSettings.Issuer,
                ValidAudience = _tokenSettings.Audience,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;

        }


        private SigningCredentials GetSigningCredentials()
        {
            byte[] key;
            if (_tokenSettings is not null)
            {
                key = Encoding.UTF8.GetBytes(_tokenSettings.SecretKey);
            }
            else
            {
                key = Encoding.UTF8.GetBytes("Tq5Dp5lzu7C9CLoYRxAU4XQsnkP2nlAnSOng4FYX");
            }
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }


        private List<Claim> GetClaims(User user)
        {
            List<Claim> cliams = new()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("Email", user.Email)
            };

            return cliams;
        }

        private async Task<List<Claim>> GetClaims(User user, IdentityUser identityUser, string userRoleEnglishName)
        {
            List<Claim> claims = GetClaims(user);
            claims.Add(new Claim("Szerep", userRoleEnglishName));

            if (_userManager != null)
            {
                var roles = await _userManager.GetRolesAsync(identityUser);
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            double expiryMinutes = 30;
            _tokenSettings ??= new TokenSettings();

            bool validExpiry = Double.TryParse(_tokenSettings.ExpiryInMinutes, out expiryMinutes);
            if (!validExpiry)
            {
                expiryMinutes = 30;
            }
            var securityToken = new JwtSecurityToken(
                       issuer: _tokenSettings.Issuer,
                       audience: _tokenSettings.Audience,
                       expires: DateTime.Now.AddMinutes(expiryMinutes),
                       signingCredentials: signingCredentials,
                       claims: claims);
            return securityToken;        
        }
    }
}
