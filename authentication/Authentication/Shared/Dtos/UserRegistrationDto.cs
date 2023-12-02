namespace Authentication.Shared.Dtos
{
    public class UserRegistrationDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{LastName} {FirstName} {Email}";
        }
    }
}
