using LibraryCore.Model;
using LibraryCore.Responses;
using LibraryDataBroker;

namespace LibraryClientServiceTemplate.ModelBrokerConnectors
{
    public interface IUpdateBrokerConnector<TEntity>
    {
        public Task<ControllerResponse> UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IDbRecord<TEntity>, new(); 
    }
}
