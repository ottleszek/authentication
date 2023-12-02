using Authentication.Shared.Models;
using LibraryDatabase.Model;

namespace Authentication.Server.Repos
{
    public interface IProfilRepo
    {
        public Task<User?> GetUserBy(string email);
        public Task<RepositoryResponse> UpdateProfil(User user);
    }
}
