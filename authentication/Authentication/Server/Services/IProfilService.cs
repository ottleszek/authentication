using Authentication.Shared.Dtos;
using LibraryDatabase.Model;

namespace Authentication.Server.Services
{
    public interface IProfilService
    {
        public Task<ProfilDto> GetUserBy(string email);
        public Task<Guid> GetUserIdBy(string email);
        public Task<string> GetProfilImageTimeStamp(string email);
        public Task<ServiceResponse> UpdateProfil(ProfilDto user);
        public Task<ServiceResponse> UpdateProfilImageTimeStamp(string email, string profilImageTimeStamp);
    }
}
