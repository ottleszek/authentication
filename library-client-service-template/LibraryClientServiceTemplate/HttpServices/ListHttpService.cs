using LibraryClientServiceTemplate.Extensions;
using LibraryCore.Model;
using LibraryDataBrokerProject;
using System.Net.Http.Json;

namespace LibraryClientServiceTemplate.HttpServices
{
    public class ListHttpService : IListDataBroker
    {
        private readonly HttpClient? _httpClient;
        private string _relativUrl = string.Empty;
        private bool HaveUrl => _relativUrl is object && _relativUrl != string.Empty;

        public ListHttpService(IHttpClientFactory httpClientFactory)
        {
            // ToDO Http clieant name is ListApiService
            _httpClient = httpClientFactory.CreateClient("AuthenticationApi");
        }

        public async Task<List<TEntity>> SelectAllRecordAsync<TEntity>() where TEntity : class, IDbRecord<TEntity>, new()
        {
            _relativUrl = RelativeUrlExtension.SetRelativeUrl<TEntity>();
            if (_httpClient is object && HaveUrl)
            {
                List<TEntity>? result = await _httpClient.GetFromJsonAsync<List<TEntity>>(_relativUrl);
                if (result is object)
                    return result;
                else
                    return new List<TEntity>();
            }
            else
                return new List<TEntity>();
        }

        public async Task<TEntity> GetBy<TEntity>(Guid id) where TEntity : class, IDbRecord<TEntity>, new()
        {
            TEntity? result = new ();
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
