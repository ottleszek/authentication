using LibraryCore.Model;
using LibraryCore.Responses;

namespace LibraryDataBroker
{
    public interface IListAndDeleteDataBroker : IListDataBroker
    {
        public Task<ControllerResponse> DeleteAsync<TEntity>(Guid id) where TEntity : class, IDbRecord<TEntity>, new();
    }
}
