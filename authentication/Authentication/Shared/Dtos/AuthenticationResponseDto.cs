using LibraryCore.Errors;

namespace Authentication.Shared.Dtos
{
    public class AuthenticationResponseDto : ErrorStore
    {
        public bool IsAuthenticationSuccessful => !HasError;

        public AuthenticationResponseDto() : base() { }
    }
}
