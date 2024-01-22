using Authentication.Shared.Dtos;
using Authentication.Shared.Model;
using LibraryCore.Responses;

namespace Authentication.Client.Library.Services.Profil
{
    public interface IProfilService
    {
        public Task<ProfilDto> GetProfilBy(string email);
        public Task<Guid> GetUserIdBy(string email);
        public Task<ControllerResponse> UpdateProfil(ProfilDto profil);

        public Task<bool> IsProfileImageExist(ProfilImageFileName profilImageFileName);
        public Task<ControllerResponse> DeleteProfilImage(ProfilImageFileName profilImageFileName);
        public Task<ControllerResponse> ProfilImageTimeStampUpdate(string email, string profilImageFileName);
    }
}
