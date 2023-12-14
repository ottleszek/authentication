﻿using LibraryClientServiceTemplate.Extensions;
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
            if (_httpClient is null)
            {
                defaultResponse = new ControllerResponse();
            }
            else
            {
                try
                {
                    HttpResponseMessage httpResponse = await _httpClient.PostAsJsonAsync(_relativUrl, entity);
                    string content = await httpResponse.Content.ReadAsStringAsync();
                    ControllerResponse? response = JsonConvert.DeserializeObject<ControllerResponse>(content);
                    if (response is not null)
                        return response;
                }
                catch(Exception ex)
                {
                    LibraryLogging.LoggingBroker.LogError($"{ex.Message}");
                }
            }
            defaultResponse.ClearAndAddError("Az adatok frissítés nem lehetséges!");
            return defaultResponse;
        }
    }
}