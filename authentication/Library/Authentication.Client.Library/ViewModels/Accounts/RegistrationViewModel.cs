using Authentication.Shared.Model;
using LibraryMvvm.Base;

namespace Authentication.Client.Library.ViewModels.Accounts
{
    public class RegistrationViewModel : ViewModelBase//, IRegistrationViewModel
    {

        public UserRegistration UserRegistration { get; set; }=new UserRegistration();

    }
}
