using LibraryCore.Model;

namespace LibraryDataBroker
{
	public interface IGetDataBroker
	{
		public Task<TEntity> GetByAsnyc<TEntity>(Guid id) where TEntity : class, IDbRecord<TEntity>, new();
		public Task<TEntity> GetByAsnyc<TEntity>(TEntity entity) where TEntity : class, IDbRecord<TEntity>, new();
	}
}
