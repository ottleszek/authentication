using LibraryCore.Model;
using LibraryCore.Responses;
using LibraryDataBroker;

namespace LibraryClientServiceTemplate.ModelBrokerConnectors
{
    public class CrudBrokerConnectorr<TEntity> : GetBrokerConnector<TEntity>, ICrudBrokerConnector<TEntity> where TEntity : class, IDbRecord<TEntity>, new()
    {
        private readonly ICrudDataBroker _broker;

        public CrudBrokerConnectorr(ICrudDataBroker broker, IGetDataBroker getBroker) : base(getBroker)
        {
            _broker = broker; 
        }

        public async Task<ControllerResponse> DeleteAsync(Guid id)
        {
            return await _broker.DeleteAsync<TEntity>(id);
        }

        public async Task<ControllerResponse> InsertAsync(TEntity entity)
        {
            return await _broker.InsertAsync<TEntity>(entity);
        }
    }
}
