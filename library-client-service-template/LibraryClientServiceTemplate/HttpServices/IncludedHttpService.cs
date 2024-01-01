using LibraryClientServiceTemplate.Extensions;
using LibraryCore.Model;
using LibraryDataBroker;
using Newtonsoft.Json;

namespace LibraryClientServiceTemplate.HttpServices
{
    public class IncludedHttpService : ListHttpService, IIncludedDataBroker
    {
        private readonly HttpClient? _httpClient;
        private string _relativUrl = string.Empty;
        private bool HaveUrl => _relativUrl is object && _relativUrl != string.Empty;

        public IncludedHttpService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthenticationApi");
        }

        public async Task<List<TEntity>> SelectAllRecordIncludedToListAsync<TEntity>() where TEntity : class, IDbRecord<TEntity>, new()
        {
            _relativUrl = RelativeUrlExtension.SetRelativeUrl<TEntity>();
            if (_httpClient is not null && HaveUrl)
            {
                HttpResponseMessage? response = await _httpClient.GetAsync($"{_relativUrl}/included");
                if (response is not null)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        List<TEntity>? result = JsonConvert.DeserializeObject<List<TEntity>>(content);
                        if (result is not null)
                            return result;
                    }
                }
            }
            return new List<TEntity>();
        }
    }
}
