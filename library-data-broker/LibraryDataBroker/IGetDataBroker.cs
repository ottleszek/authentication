using LibraryCore.Model;

namespace LibraryDataBroker
{
	public interface IGetDataBroker
	{
		public Task<TEntity> GetByIdAsnyc<TEntity>(Guid id) where TEntity : class, IDbRecord<TEntity>, new();
	}
}
