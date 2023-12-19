using LibraryCore.Model;
using LibraryCore.Responses;

namespace LibraryDataBroker
{
	public interface IUpdateDataBroker : IGetDataBroker
	{
		public Task<ControllerResponse> UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IDbRecord<TEntity>, new();
	}
}
