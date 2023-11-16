using AuthenticationLibrary.Shared.Dtos;

namespace AuthenticationLibrary.Services.Accounts
{
    public interface IRegistrationService
    {
        public Task<AuthenticationResponseDto> UserRgistration(UserRegistrationDto user);
    }
}
