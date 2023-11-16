using LibraryCore.Errors;

namespace AuthenticationLibrary.Shared.Dtos
{
    public class AuthenticationResponseDto : ErrorStore
    {
        public bool IsAuthenticationSuccessful => !HasError;

        public AuthenticationResponseDto() : base() { }
    }
}
