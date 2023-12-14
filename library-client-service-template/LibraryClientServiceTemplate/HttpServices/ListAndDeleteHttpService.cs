using LibraryCore.Model;
using LibraryCore.Responses;
using LibraryDataBroker;

namespace LibraryClientServiceTemplate.HttpServices
{
    public class ListAndDeleteHttpService : ListHttpService, IListAndDeleteDataBroker
    {
        private readonly HttpClient? _httpClient;
        private readonly ICrudDataBroker _crudHttpBroker;

        public ListAndDeleteHttpService(ICrudDataBroker crudHttpBroker, IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthenticationApi");
            _crudHttpBroker = crudHttpBroker;
        }

        public Task<ControllerResponse> DeleteAsync<TEntity>(Guid id) where TEntity : class, IDbRecord<TEntity>, new()
        {
            return _crudHttpBroker.DeleteAsync<TEntity>(id);
        }
    }
}
