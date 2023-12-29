using Authentication.Shared.Models;

namespace Authentication.Server.Repos
{
    public interface IUserRepo
    {
        public Task<List<TEntity>> SelectAllUserIncludedAsync<TEntity>(long schoolClassId) where TEntity : User, new();
    }
}
