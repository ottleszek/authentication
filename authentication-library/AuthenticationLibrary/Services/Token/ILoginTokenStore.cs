using LibraryCore.Errors;

namespace AuthenticationLibrary.Services.Token
{
    public interface ILoginTokenStore
    {
        public Task<ErrorStore> SaveAccessTokenAndRefreshToken(string accessJwtToken, string refreshToken);
        public Task<ErrorStore> DeleteAccessTokenAndRefreshToken();
    }
}
