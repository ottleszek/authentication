using LibraryCore.Model;
using LibraryDataBroker;
using LibraryDataBrokerProject;

namespace LibraryClientServiceTemplate.ModelBrokerConnectors
{
	public class GetBrokerConnector<TEntity> : IGetBrokerConnector<TEntity> where TEntity : class, IDbRecord<TEntity>, new()
	{
		private readonly IGetDataBroker _broker;

        public GetBrokerConnector(IGetDataBroker borker)
        {
			_broker = borker;
        }

		public async Task<TEntity> GetByAsnyc(Guid id)
		{
			return await _broker.GetByAsnyc<TEntity>(id);
		}

		public async Task<TEntity> GetByAsnyc(TEntity entity)
		{
			return await _broker.GetByAsnyc<TEntity>(entity);
		}
	}
}
