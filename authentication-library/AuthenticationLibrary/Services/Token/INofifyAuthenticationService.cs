namespace AuthenticationLibrary.Services.Token
{
    public interface INofifyAuthenticationService
    {
        public void NotifyUseAuthentication(string accessJwtToken);
        public void NotifyLogOut();
    }
}
