using Authentication.Shared.Dtos;
using LibraryCore.Responses;

namespace Authentication.Client.Library.Services.Profil
{
    public interface IProfilService
    {
        public Task<ProfilDto> GetProfilBy(string email);
        public Task<Guid> GetUserIdBy(string email);
        public Task<ControllerResponse> UpdateProfil(ProfilDto profil);
    }
}
