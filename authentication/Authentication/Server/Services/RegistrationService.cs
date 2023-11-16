using Authentication.Server.Repos;
using Authentication.Shared.Models;
using AuthenticationLibrary.Shared.Dtos;
using LibaryDatabase.Model;

using LibraryPassword;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Server.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IAccountRepo _accountRepo;
        private readonly IUserRoleRepo? _userRoleRepo;
        private readonly IUserIdentificationRepo _userIdentificationRepo;
        private readonly UserManager<IdentityUser> _userManager;

        public RegistrationService(IAccountRepo accountRepo, IUserRoleRepo? userRoleRepo, IUserIdentificationRepo userIdentificationRepo, UserManager<IdentityUser> userManager)
        {
            _accountRepo = accountRepo;
            _userRoleRepo = userRoleRepo;   
            _userManager = userManager;
            _userIdentificationRepo = userIdentificationRepo;
        }

        public async Task<AuthenticationResponseDto> RegisterNewUser(UserRegistrationDto registrationPlayload)
        {
            AuthenticationResponseDto responseDto = new ();

            if (_accountRepo.IsUserExsist(registrationPlayload.Email))
            {
                responseDto.ClearAndAddError($"{registrationPlayload.Email} emailcímmel már regisztráltak felhasználót!");
                return responseDto;
            }

            RepositoryResponse response = new ();
            try
            {
                if (_userRoleRepo is not null)
                {
                    // Regisztáció saját User táblába látogatóként ("viewer")
                    Guid? viewerRoleId = _userRoleRepo.GetByEnglishName("viewer");
                    if (viewerRoleId is null)
                    {
                        viewerRoleId = Guid.Empty;
                    }
                    User newUser = DtoToUserModel(registrationPlayload);
                    
                    newUser.Id = new Guid();
                    newUser.UserRoleId = (Guid) viewerRoleId;
                    string password = PasswordExtension.HashPassword(registrationPlayload.Password);
                    
                    RepositoryResponse saveUserResponse = await _accountRepo.Save(newUser);                    
                    if (saveUserResponse.HasError )
                    {
                        return new AuthenticationResponseDto
                        {
                            Error = saveUserResponse.Error
                        };
                    }
                    RepositoryResponse savePasswordResponse = await _userIdentificationRepo.Save(newUser.Id, password);
                    if (savePasswordResponse.HasError)
                    {
                        return new AuthenticationResponseDto
                        {
                            Error = savePasswordResponse.Error
                        };
                    }

                    // Regisztráció az UserManager által kezelt táblába látogatóként ("viewer")
                    var user = new IdentityUser { UserName = registrationPlayload.Email, Email = registrationPlayload.Email };
                    var identityResult = await _userManager.CreateAsync(user, registrationPlayload.Password);
                    if (!identityResult.Succeeded)
                    {
                        var errors = identityResult.Errors.Select(e => e.Description).First();
                        responseDto.ClearAndAddError(errors);
                        return responseDto;
                    }
                    await _userManager.AddToRoleAsync(user, "Viewer");
                }
            }
            catch(Exception ex)
            {
                LibraryLogging.LoggingBroker.LogError(ex.Message);
                responseDto.ClearAndAddError("Felhasználó regisztrálása nem lehetséges! Próbálja meg újra később!");
            }
            if (response.InstructionSuccessfullyExecuted)
            {
                LibraryLogging.LoggingBroker.LogDebug($"{registrationPlayload} adatokkal felhasználó sikeresen regisztrálva.");
            }
            else
            {
                LibraryLogging.LoggingBroker.LogError($"Felhasználó regisztrálása nem lehetséges!");
                responseDto.ClearAndAddError("Felhasználó regisztrálása nem lehetséges! Próbálja meg újra később!");
            }
            return responseDto;
        }        

        public bool ChaeckUniqueUserEmail(string email)
        {
            bool userAlredyExsist = _accountRepo.IsUserExsist(email);
            return !userAlredyExsist;
        }

        private static User DtoToUserModel(UserRegistrationDto userRegistrationDto)
        {
            return new User
            {
                FirstName = userRegistrationDto.FirstName,
                LastName = userRegistrationDto.LastName,
                Email = userRegistrationDto.Email,
            };
        }
    }
}
