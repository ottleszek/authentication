using LibraryCore.Model;
using LibraryCore.Responses;
using LibraryDataBroker;

namespace LibraryClientServiceTemplate.ModelBrokerConnectors
{
    public class UpdateBrokerConnector<TEntity> : IUpdateBrokerConnector<TEntity> where TEntity : class, IDbRecord<TEntity>, new()
    {
        private readonly IUpdateDataBroker _broker;

        public UpdateBrokerConnector(IUpdateDataBroker broker)
        {
            _broker = broker;
        }

        public async Task<ControllerResponse> UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IDbRecord<TEntity>, new()
        {
            return await _broker.UpdateAsync<TEntity>(entity);
        }
    }
}
