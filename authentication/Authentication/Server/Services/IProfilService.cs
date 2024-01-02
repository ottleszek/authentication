using Authentication.Shared.Dtos;
using Authentication.Shared.Models;
using LibraryDatabase.Model;

namespace Authentication.Server.Services
{
    public interface IProfilService
    {
        public Task<ProfilDto> GetUserBy(string email);
        public Task<Guid> GetUserIdBy(string email);
        public Task<ServiceResponse> UpdateProfil(ProfilDto user);
    }
}
