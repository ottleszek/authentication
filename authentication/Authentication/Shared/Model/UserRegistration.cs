
using Authentication.Shared.Dtos;

namespace Authentication.Shared.Model
{
    public class UserRegistration
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;

        public UserRegistrationDto CopyToDto()
        {
            return new UserRegistrationDto
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password
            };
        }


        public override string ToString()
        {
            return $"{LastName} {FirstName} {Email}";
        }
    }
}
