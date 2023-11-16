using AuthenticationLibrary.Shared.Dtos;

namespace Authentication.Server.Services
{
    public interface IRegistrationService
    { 
        public Task<AuthenticationResponseDto> RegisterNewUser(UserRegistrationDto registrationPlayload);
        public bool ChaeckUniqueUserEmail(string email);
    }
}
