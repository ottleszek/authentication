using LibraryClientServiceTemplate.Extensions;
using LibraryCore.Model;
using LibraryCore.Responses;
using LibraryDataBroker;
using Newtonsoft.Json;
using System.Net;
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

                    if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                    {
                        string content = await httpResponse.Content.ReadAsStringAsync();
                        ControllerResponse? response = JsonConvert.DeserializeObject<ControllerResponse>(content);
                        if (response is null)
                        {
                            defaultResponse.ClearAndAddError("A módosítás http kérés hibát okozott!");
                        }
                        else return response;
                    }
                    else if (!httpResponse.IsSuccessStatusCode)
                    {
                        httpResponse.EnsureSuccessStatusCode();
                    }
                    else
                    {
                        return defaultResponse;
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
