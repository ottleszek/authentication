using Authentication.Shared.Dtos;
using LibraryMvvm.Base;

namespace Authentication.Client.Library.ViewModels.Accounts
{
    public class RegistrationViewModel : ViewModelBase
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
    }
}
