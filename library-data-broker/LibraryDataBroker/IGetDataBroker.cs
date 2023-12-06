using LibraryCore.Model;

namespace LibraryDataBroker
{
	public interface IGetDataBroker
	{
		public Task<TEntity> GetByAsnyc<TEntity>(Guid id) where TEntity : class, IDbRecord<TEntity>, new();
	}
}
