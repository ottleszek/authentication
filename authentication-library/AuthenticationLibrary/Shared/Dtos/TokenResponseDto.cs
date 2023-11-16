using LibraryCore.Errors;

namespace AuthenticationLibrary.Shared.Dtos
{
    public class TokenResponseDto : ErrorStore
    {
        private string _accessToken = string.Empty;
        public string AccessToken
        {
            get => _accessToken;
            set
            {
                ClearErrorStore();
                _accessToken = value;
            }
        }

        private string _refreshToken = string.Empty;
        public string RefreshToken 
        {
            get => _refreshToken; 
            set
            {
                ClearErrorStore();
                _refreshToken = value;
            }
        }

        public TokenResponseDto() : base() { }

        public bool IsLoggedIn => !HasError;
        public bool HasAccessToken => !string.IsNullOrEmpty(AccessToken);
        public bool HasRefreshToken => !string.IsNullOrEmpty(RefreshToken); 


    }
}
