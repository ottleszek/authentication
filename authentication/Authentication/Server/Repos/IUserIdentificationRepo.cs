using Authentication.Server.Datas.Entities;
using LibaryDatabase.Model;

namespace Authentication.Server.Repos
{
    public interface IUserIdentificationRepo
    {
        public string? GetPassword(Guid id);

        public Task<RepositoryResponse> Save(Guid userId, string password);
    }
}
