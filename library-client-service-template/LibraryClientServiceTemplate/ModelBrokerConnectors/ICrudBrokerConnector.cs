using LibraryCore.Responses;

namespace LibraryClientServiceTemplate.ModelBrokerConnectors
{
    public interface ICrudBrokerConnector<TEntity>
    {
        public Task<ControllerResponse> InsertAsync(TEntity entity);
        public Task<ControllerResponse> DeleteAsync(Guid id);
    }
}
