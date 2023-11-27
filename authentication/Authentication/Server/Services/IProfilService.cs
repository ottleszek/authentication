using Authentication.Shared.Models;
using LibraryDatabase.Model;

namespace Authentication.Server.Services
{
    public interface IProfilService
    {
        public Task<User> GetUserBy(string email);
        public Task<ServiceResponse> UpdateProfil(User user);
    }
}
