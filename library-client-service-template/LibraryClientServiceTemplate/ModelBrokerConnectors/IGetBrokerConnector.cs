namespace LibraryClientServiceTemplate.ModelBrokerConnectors
{
    public interface IGetBrokerConnector<TEntity>
	{
		public Task<TEntity> GetByAsnyc(Guid id);
	}
}
