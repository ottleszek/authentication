using LibraryClientServiceTemplate.Extensions;
using LibraryCore.Model;
using LibraryDataBroker;
using Newtonsoft.Json;

namespace LibraryClientServiceTemplate.HttpServices
{
    public class ListHttpService : GetHttpService,  IListDataBroker
    {
        private readonly HttpClient? _httpClient;
        private string _relativUrl = string.Empty;
        private bool HaveUrl => _relativUrl is object && _relativUrl != string.Empty;

        public ListHttpService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthenticationApi");
        }

        public async Task<List<TEntity>> SelectAllRecordAsync<TEntity>() where TEntity : class, IDbRecord<TEntity>, new()
        {
            _relativUrl = RelativeUrlExtension.SetRelativeUrl<TEntity>();
            if (_httpClient is not null && HaveUrl)
            {
                try
                {
                    HttpResponseMessage? response = await _httpClient.GetAsync(_relativUrl);
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
                catch (Exception ex)
                {
                    LibraryLogging.LoggingBroker.LogError(nameof(ListHttpService), nameof(SelectAllRecordAsync), ex.Message);
                }
            }
            return new List<TEntity>();
        }
    }
}
