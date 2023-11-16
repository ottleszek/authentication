namespace Authentication.Client.Library.Components.UserIdentification
{
    public class UserDataFromClaim
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> UserRole = new List<string>();
    }
}
