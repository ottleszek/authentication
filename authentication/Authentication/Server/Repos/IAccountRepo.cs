using Authentication.Shared.Models;
using LibaryDatabase.Model;

namespace Authentication.Server.Repos
{
    public interface IAccountRepo
    {
        public bool IsUserExsist(string email);

        public User? GetUserBy(string email);
        public User? GetUserBy(Guid userId);

        public bool UserAddToRole(Guid userId, string roleEnglishName);

        public Task<RepositoryResponse> SaveNewUser(User user);
    }
}
