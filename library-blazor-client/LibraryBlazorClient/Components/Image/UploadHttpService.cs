﻿
using System.Net.Http;

namespace LibraryBlazorClient.Components.Image
{
    public class UploadHttpService
    {
        private readonly HttpClient? _httpClient;
        private string _relativUrl = string.Empty;
        private bool HaveUrl => _relativUrl is object && _relativUrl != string.Empty;

        public UploadHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthenticationApi");
        }

        public async Task<string> UploadImage(MultipartFormDataContent content)
        {
            if (_httpClient is not null)
            {
                var postResult = await _httpClient.PostAsync($"{_relativUrl}/api/upload", content);
                var postContent = await postResult.Content.ReadAsStringAsync();
                if (!postResult.IsSuccessStatusCode)
                {
                    throw new ApplicationException(postContent);
                }
                else
                {
                    var imgUrl = Path.Combine($"{_relativUrl}", postContent);
                    return imgUrl;
                }
            }
            return string.Empty;
        }
    }
}