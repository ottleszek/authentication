using Authentication.Shared.Dtos;

namespace Authentication.Client.Library.ViewModels.User
{
    public interface IProfilViewModel
    {
        public Task GetProfilBy(string email);
        public Task UpdateProfil();
    }
}
