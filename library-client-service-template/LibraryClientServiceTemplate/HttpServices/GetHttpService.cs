using LibraryClientServiceTemplate.Extensions;
using LibraryCore.Model;
using LibraryDataBroker;
using System.Net.Http.Json;

namespace LibraryClientServiceTemplate.HttpServices
{
    public class GetHttpService : IGetDataBroker
    {
        private readonly HttpClient? _httpClient;
        private string _relativUrl = string.Empty;
        private bool HaveUrl => _relativUrl is object && _relativUrl != string.Empty;

        public GetHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthenticationApi");
        }

        public async Task<TEntity> GetByAsnyc<TEntity>(Guid id) where TEntity : class, IDbRecord<TEntity>, new()
        {
            TEntity? result = new();
            _relativUrl = RelativeUrlExtension.SetRelativeUrl<TEntity>();
            if (_httpClient is object && HaveUrl)
            {
                result = await _httpClient.GetFromJsonAsync<TEntity>($"{_relativUrl}/{id}");
                if (result is object)
                    return result;
                else
                    result = new TEntity();
            }
            return result;
        }
    }
}
