using Authentication.Server.Datas.Entities;
using Authentication.Server.Repos;
using Authentication.Shared.Models;
using AuthenticationLibrary.Shared.Dtos;
using LibraryPassword;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Server.Services
{
    public class LoginService : ILoginService
    {
        private readonly IAccountRepo? _accountRepo;
        private readonly IUserRefreshTokenRepo? _userRefreshTokenRepo;
        private readonly IUserRoleRepo? _userRoleRepo;
        private readonly IUserIdentificationRepo? _userIdentificationRepo;
        private readonly ITokenService _tokenService;        
        private readonly UserManager<IdentityUser>? _userManager;

        public LoginService(ITokenService tokenService, IAccountRepo accoutRepo, IUserRefreshTokenRepo userRefreshTokenRepo, IUserRoleRepo? userRoleRepo,IUserIdentificationRepo userIdentificationRepo, UserManager<IdentityUser> userManager)
        {
            _accountRepo = accoutRepo;
            _userRefreshTokenRepo = userRefreshTokenRepo;
            _tokenService = tokenService;
            _userRoleRepo = userRoleRepo;
            _userIdentificationRepo = userIdentificationRepo;
            _userManager = userManager;
        }

        public async Task<TokenResponseDto> Login(UserLoginDto loginPlayload)
        {
            TokenResponseDto response = new();

            if (string.IsNullOrEmpty(loginPlayload.Email) || (string.IsNullOrEmpty(loginPlayload.Password)))
            {
                response.ClearAndAddError("Üres bejelentkezési adatok!");
            }

            IdentityUser? identityUser = null;
            if (_userManager is not null)
            {
                identityUser = await _userManager.FindByNameAsync(loginPlayload.Email);
            }

            if (identityUser is null)
            {
                response.ClearAndAddError("Identity user azonosítása nem sikerült!");
                return response;
            }

            if (_accountRepo is not null && _userRoleRepo is not null)
            {

                User? user = await _accountRepo.GetUserBy(loginPlayload.Email);
                if (user == null)
                {
                    response.ClearAndAddError("Evvel az email címmel nem található felhasználó!");
                }
                else
                {
                    string? userPassword = string.Empty;
                    if (_userIdentificationRepo is not null)
                    {
                        userPassword = _userIdentificationRepo.GetPassword(user.Id);
                    }
                    if (userPassword is null)
                    {
                        response.ClearAndAddError("A felhasználó jelszava nem található!");
                    }
                    else
                    {
                        bool validPassword = PasswordVerification(loginPlayload.Password, userPassword);
                        if (!validPassword)
                        {
                            response.ClearAndAddError("A megadott jelszó nem megfelelő!");
                        }
                        else
                        {

                            string userEnglishRoleName = _userRoleRepo.GetEnglishNameBy(user.UserRoleId);
                            // JWT token
                            string token = await _tokenService.GenerateJwtToken(user, identityUser, userEnglishRoleName);
                            // User refresh token
                            string? refreshToken = await GenerateAndSaveRefreshToken(user.Id);

                            if (string.IsNullOrEmpty(refreshToken))
                            {
                                response.ClearAndAddError("A bejelentkezés nem lehetséges, próbálja meg később!");
                            }
                            else
                            {
                                response = new TokenResponseDto
                                {
                                    AccessToken = token,
                                    RefreshToken = refreshToken,
                                };
                                response.ClearErrorStore();
                            }
                        }
                    }
                }
            }
            return response;
        }

        public async Task<TokenResponseDto> RenewTokenAsnyc(RefreshTokenDto? refreshTokenDto)
        {
            TokenResponseDto response = new();
            if (refreshTokenDto is not null && _accountRepo is not null)
            {
                User? user = _accountRepo.GetUserBy(refreshTokenDto.UserId);
                if (user != null && _userRefreshTokenRepo is not null)
                {
                    UserRefreshToken? oldUserRefreshToken = await _userRefreshTokenRepo.GetRefreshToken(refreshTokenDto);
                    if (oldUserRefreshToken is not null)
                    {
                        try
                        {
                            await _userRefreshTokenRepo.DeleteRefreshToken(oldUserRefreshToken);
                            // JWT token
                            string token = _tokenService.GenerateJwtToken(user);
                            // User refresh token
                            string? refreshToken = await GenerateAndSaveRefreshToken(user.Id);
                            if (!string.IsNullOrEmpty(refreshToken))
                            {
                                response = new TokenResponseDto
                                {
                                    AccessToken = token,
                                    RefreshToken = refreshToken,
                                };
                                return response;
                            }
                        }
                        catch (Exception ex)
                        {
                            LibraryLogging.LoggingBroker.LogError(ex.Message);
                        }
                    }
                }
            }
            response.ClearAndAddError("Érvénytelen felhasználói hozzáférés!");
            return response;
        }

        private async Task<string?> GenerateAndSaveRefreshToken(Guid UserId)
        {
            UserRefreshToken userRefreshToken = GenerateRefreshToken(UserId);
            if (!await SaveRefreshToken(userRefreshToken))
            {
                return null;
            }
            return userRefreshToken.Token;
        }

        private  UserRefreshToken GenerateRefreshToken(Guid userId)
        {
            string refreshToken = _tokenService.GenerateRefreshToken();
            UserRefreshToken userRefreshToken = new UserRefreshToken
            {
                Token = refreshToken,
                UserId = userId,
                ExpirationDate = DateTime.Now.AddDays(3)

            };
            return userRefreshToken;
        }

        private async Task<bool> SaveRefreshToken(UserRefreshToken token)
        {
            if (_userRefreshTokenRepo is not null)
            {
                try
                {
                    await _userRefreshTokenRepo.SaveRefreshToken(token);
                }
                catch(Exception ex) 
                {
                    LibraryLogging.LoggingBroker.LogError(ex.Message);
                    return false;
                }
            }
            return true;
        }

        private bool PasswordVerification(string plainPassword, string? dbPassword)
        {
            PasswordManager pm = new PasswordManager();
            if (string.IsNullOrEmpty(plainPassword) || string.IsNullOrEmpty(dbPassword))
                return false;            
            return pm.VerifyPassword(plainPassword, dbPassword);
        }
    }
}
