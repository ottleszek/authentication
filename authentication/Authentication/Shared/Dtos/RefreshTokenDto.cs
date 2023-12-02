namespace Authentication.Shared.Dtos
{
    public class RefreshTokenDto
    {
        public Guid UserId { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
