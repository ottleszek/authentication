using LibraryDataBrokerProject;

namespace LibraryClientServiceTemplate.ModelBrokerConnectors
{
    public interface IGetBrokerConnector<TEntity>
	{
		public Task<TEntity> GetByAsnyc(Guid id);
		public Task<TEntity> GetByAsnyc(TEntity entity);
	}
}
