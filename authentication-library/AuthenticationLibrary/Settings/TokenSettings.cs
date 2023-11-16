namespace AuthenticationLibrary.Settings
{
    public class TokenSettings
    {
        public string SecretKey { get; set; } = "Tq5Dp5lzu7C9CLoYRxAU4XQsnkP2nlAnSOng4FYX";
        public string Issuer { get; set; } = "kreata.hu";
        public string Audience { get; set; } = "https://localhost:7220";
        public string ExpiryInMinutes { get; set; } = "5";

        public bool HaveExpiryInMinutes => !string.IsNullOrEmpty(ExpiryInMinutes);
    }
}
