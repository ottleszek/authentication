using Authentication.Shared.Dtos;
using LibraryDatabase.Model;

namespace Authentication.Server.Services
{
    public interface IProfilService
    {
        public Task<ProfilDto> GetUserBy(string email);
        public Task<Guid> GetUserIdBy(string email);
        public Task<ServiceResponse> UpdateProfil(ProfilDto user);
        //public Task<ServiceResponse> UpdateProfilImage(string email, string profilImageUrl);
    }
}
