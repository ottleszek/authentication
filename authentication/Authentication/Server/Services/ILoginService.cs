using Authentication.Shared.Dtos;

namespace Authentication.Server.Services
{
    public interface ILoginService
    { 
        public Task<TokenResponseDto> Login(UserLoginDto loginPlayload);
        public Task<TokenResponseDto> RenewTokenAsnyc(RefreshTokenDto? refreshTokenDto);
    }
}
