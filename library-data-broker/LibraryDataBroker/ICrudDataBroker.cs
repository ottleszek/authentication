using LibraryCore.Model;
using LibraryCore.Responses;

namespace LibraryDataBroker
{
	public interface ICrudDataBroker : IUpdateDataBroker
	{
		public Task<ControllerResponse> InsertAsync<TEntity>(TEntity entity) where TEntity : class, IDbRecord<TEntity>, new();
		public Task<ControllerResponse> DeleteAsync<TEntity>(Guid id) where TEntity : class, IDbRecord<TEntity>, new();
	}
}
