using Authentication.Shared.Models;
using LibaryDatabase.Model;

namespace Authentication.Server.Repos
{
    public interface IProfilRepo
    {
        public User? GetUserBy(string email);
        public Task<RepositoryResponse> UpdateProfil(User user);
    }
}
