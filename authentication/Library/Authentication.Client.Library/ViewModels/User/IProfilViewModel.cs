using Authentication.Shared.Dtos;

namespace Authentication.Client.Library.ViewModels.User
{
    public interface IProfilViewModel
    {
        public string Email { get; set; }
        public ProfilDto ProfilDto { get; set; }

        public Task UpdateProfil();
    }
}
