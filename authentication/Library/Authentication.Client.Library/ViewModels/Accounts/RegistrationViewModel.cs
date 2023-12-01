using Authentication.Shared.Model;
using LibraryMvvm.Base;

namespace Authentication.Client.Library.ViewModels.Accounts
{
    public class RegistrationViewModel : ViewModelBase//, IRegistrationViewModel
    {

        public UserRegistration UserRegistration { get; set; }=new UserRegistration();
        /*
        public string FirstName  { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } =string.Empty;

        public UserRegistrationDto ConverToUserRegistrationDto => new()
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Password = Password,
            ComfirmPassword = ConfirmPassword
        };*/
    }
}
