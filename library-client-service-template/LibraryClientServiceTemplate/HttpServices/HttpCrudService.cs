using LibraryClientServiceTemplate.Extensions;
using LibraryCore.Model;
using LibraryCore.Responses;
using LibraryDataBroker;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace LibraryClientServiceTemplate.HttpServices
{
    public class HttpCrudService : UpdateHttpService, ICrudDataBroker
    {
        private readonly HttpClient? _httpClient;
        private string _relativUrl = string.Empty;
        private bool HaveUrl => _relativUrl is object && _relativUrl != string.Empty;

        public HttpCrudService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthenticationApi");
        }

        public async Task<ControllerResponse> DeleteAsync<TEntity>(Guid id) where TEntity : class, IDbRecord<TEntity>, new()
        {
            _relativUrl = RelativeUrlExtension.SetRelativeUrl<TEntity>();
            ControllerResponse defaultResponse = new();
            if (_httpClient is object && HaveUrl)
            {
                try
                {
                    HttpResponseMessage httpResponse = await _httpClient.DeleteAsync($"{_relativUrl}/{id}");
                    string content = await httpResponse.Content.ReadAsStringAsync();
                    ControllerResponse? response = JsonConvert.DeserializeObject<ControllerResponse>(content);
                    if (response is not null)
                        return response;
                }
                catch (Exception ex)
                {
                    LibraryLogging.LoggingBroker.LogError($"{ex.Message}");
                }
                defaultResponse.ClearAndAddError("Az adatok frissítés nem lehetséges!");
                return defaultResponse;
            }
        }

        public async Task<ControllerResponse> InsertAsync<TEntity>(TEntity entity) where TEntity : class, IDbRecord<TEntity>, new ()
        {
            _relativUrl = RelativeUrlExtension.SetRelativeUrl<TEntity>();
            ControllerResponse defaultResponse = new();
            if (_httpClient is object && HaveUrl)
            {
                HttpResponseMessage? httpResponse = null;
                try
                {
                    if (entity.HasId)
                    {
                        httpResponse = await _httpClient.PutAsJsonAsync(_relativUrl, entity);
                    }
                    else
                    {
                        httpResponse = await _httpClient.PostAsJsonAsync(_relativUrl, entity);
                    }
                    string content = await httpResponse.Content.ReadAsStringAsync();
                    ControllerResponse? response = JsonConvert.DeserializeObject<ControllerResponse>(content);
                    if (response is not null)
                        return response;
                }
                catch (Exception ex)
                {
                    LibraryLogging.LoggingBroker.LogError($"{ex.Message}");
                }
            }
            defaultResponse.ClearAndAddError("Az adatok frissítés nem lehetséges!");
            return defaultResponse;
        }
    }
}
