using LibraryCore.Model;
using LibraryDataBroker;

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
			return await _broker.GetByIdAsnyc<TEntity>(id);
		}
	}
}
