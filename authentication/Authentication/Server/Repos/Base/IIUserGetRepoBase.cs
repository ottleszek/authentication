using Authentication.Shared.Models;

namespace Authentication.Server.Repos.Base
{
    public interface IIUserGetRepoBase
    {
        public bool IsUserExsist(string email);
        public User? GetUserBy(string email);
    }
}
