using Authentication.Server.Repos.Base;
using Authentication.Shared.Models;
using LibraryDatabase.Model;

namespace Authentication.Server.Repos
{
    public interface IProfilRepo : IIUserGetRepoBase
    {
        public Task<RepositoryResponse> UpdateProfil(User user);
    }
}
