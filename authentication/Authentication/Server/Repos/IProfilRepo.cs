using Authentication.Server.Repos.Base;
using Authentication.Shared.Models;
using LibraryDatabase.Model;

namespace Authentication.Server.Repos
{
    public interface IProfilRepo : IIUserGetRepoBase
    {
        public Task<RepositoryResponse> UpdateProfil(User user);
        public Task<RepositoryResponse> UpdateProfilImageTimeStamp(string email, string profilImageTimeStamp);
        public Task<string> GetProfilImageTimeStamp(string email);
    }
}
