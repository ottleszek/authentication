using LibraryClientServiceTemplate.Extensions;
using LibraryCore.Model;
using LibraryCore.Responses;
using LibraryDataBroker;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace LibraryClientServiceTemplate.HttpServices
{
    public class UpdateHttpService : GetHttpService,  IUpdateDataBroker
    {
        private readonly HttpClient? _httpClient;
        private string _relativUrl = string.Empty;
        private bool HaveUrl => _relativUrl is object && _relativUrl != string.Empty;

        public UpdateHttpService(IHttpClientFactory httpClientFactory) 
            :base(httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthenticationApi");
        }

        public async Task<ControllerResponse> UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IDbRecord<TEntity>, new()
        {
            _relativUrl = RelativeUrlExtension.SetRelativeUrl<TEntity>();
            ControllerResponse defaultResponse = new();
            if (_httpClient is not null)
            {
                try
                {
                    HttpResponseMessage httpResponse = await _httpClient.PutAsJsonAsync(_relativUrl, entity);
                    string content = await httpResponse.Content.ReadAsStringAsync();
                    ControllerResponse? response = JsonConvert.DeserializeObject<ControllerResponse>(content);
                    if (response is not null)
                    {
                        if (response.IsSuccess)
                        {
                            return defaultResponse;
                        }
                        else
                        {
                            LibraryLogging.LoggingBroker.LogError($"{response.Message}");
                        }
                    }
                }
                catch(Exception ex)
                {
                    LibraryLogging.LoggingBroker.LogError(nameof(UpdateHttpService),nameof(UpdateAsync),ex.Message);
                }
            }
            defaultResponse.ClearAndAddError("Az adatok frissítés nem lehetséges!");
            return defaultResponse;
        }
    }
}
