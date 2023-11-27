using Authentication.Shared.Dtos;

namespace Authentication.Client.Library.ViewModels.User
{
    public interface IProfilViewModel
    {
        public ProfilDto ProfilDto { get; set; }

        public Task GetProfilBy(string email);
        public Task UpdateProfil();
    }
}
