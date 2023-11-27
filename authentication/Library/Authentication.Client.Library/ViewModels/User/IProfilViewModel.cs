using Authentication.Shared.Dtos;

namespace Authentication.Client.Library.ViewModels.User
{
    public interface IProfilViewModel
    {
        public ProfilDto GetProfilBy(string email);
    }
}
