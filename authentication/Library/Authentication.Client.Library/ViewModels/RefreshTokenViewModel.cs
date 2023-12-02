namespace Authentication.Client.Library.ViewModels
{
    public class RefreshTokenViewModel
    {
        public Guid UserId { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
