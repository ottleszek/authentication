using Authentication.Shared.Models;
using LibraryDataBroker;

namespace Authentication.Server.Repos
{
    public interface IUserRepo : IIncludedDataBroker
    {
        public Task<List<TEntity>> SelectAllUserIncludedAsync<TEntity>(long schoolClassId) where TEntity : User, new();
    }
}
