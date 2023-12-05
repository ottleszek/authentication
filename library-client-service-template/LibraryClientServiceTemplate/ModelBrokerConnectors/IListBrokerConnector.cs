namespace LibraryClientServiceTemplate.ModelBrokerConnectors
{
    public interface IListBrokerConnector<TItem> : IGetBrokerConnector<TItem>
	{
		public Task<List<TItem>> SelectAllRecordAsync();
	}
}
