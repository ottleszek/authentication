using Authentication.Shared.Dtos;

namespace Authentication.Client.Library.ViewModels.Accounts
{
    public interface IRegistrationViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public string ConfirmPassword { get; set; }

        public UserRegistrationDto ConverToUserRegistrationDto { get; }
        
    }
}
