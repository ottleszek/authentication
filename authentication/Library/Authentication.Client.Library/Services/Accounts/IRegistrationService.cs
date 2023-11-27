using Authentication.Shared.Dtos;

namespace Authentication.Shared.Services.Accounts
{
    public interface IRegistrationService
    {
        public Task<AuthenticationResponseDto> UserRgistration(UserRegistrationDto user);
    }
}
