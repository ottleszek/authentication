using AuthenticationLibrary.Shared.Dtos;
using LibraryCore.Errors;

namespace AuthenticationLibrary.Services.Accounts
{
    public interface IAuthenticationService
    {
        public Task<ErrorStore> LoginAsync(UserLoginDto loginPlayload);
        public Task Logout();
    }
}
