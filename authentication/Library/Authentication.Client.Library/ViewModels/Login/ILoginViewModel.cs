using Authentication.Shared.Dtos;

namespace Authentication.Client.Library.ViewModels.Login
{
    public interface ILoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public UserLoginDto ConvertToUserLoginDto { get; }
    }
}
