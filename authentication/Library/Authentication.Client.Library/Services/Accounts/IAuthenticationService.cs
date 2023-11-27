using Authentication.Shared.Dtos;
using LibraryCore.Errors;

namespace Authentication.Shared.Services.Accounts
{
    public interface IAuthenticationService
    {
        public Task<ErrorStore> LoginAsync(UserLoginDto loginPlayload);
        public Task Logout();
    }
}
