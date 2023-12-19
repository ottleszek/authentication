using LibraryCore.Responses;

namespace LibraryClientServiceTemplate.ModelBrokerConnectors
{
    public interface IListAndDeleteBrokerConnector<TEntity> : IListBrokerConnector<TEntity>
    {
        public Task<ControllerResponse> DeleteAsync(Guid id);
    }
}
