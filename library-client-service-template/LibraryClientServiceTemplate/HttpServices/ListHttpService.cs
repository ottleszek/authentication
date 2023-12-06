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
            _httpClient = httpClientFactory.CreateClient("AuthenticationApi");
        }

        public async Task<List<TEntity>> SelectAllRecordAsync<TEntity>() where TEntity : class, IDbRecord<TEntity>, new()
        {
            _relativUrl = RelativeUrlExtension.SetRelativeUrl<TEntity>();
            if (_httpClient is not null && HaveUrl)
            {
                List<TEntity>? result = await _httpClient.GetFromJsonAsync<List<TEntity>>(_relativUrl);
                if (result is not null)
                    return result;
                else
                    return new List<TEntity>();
            }
            else
                return new List<TEntity>();
        }


    }
}
