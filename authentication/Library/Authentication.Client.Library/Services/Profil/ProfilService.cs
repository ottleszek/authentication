using Authentication.Shared.Dtos;
using LibraryCore.Responses;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Authentication.Client.Library.Services.Profil
{
    public class ProfilService : IProfilService
    {
        private readonly HttpClient _httpClient;

        public ProfilService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthenticationApi");
        }

        public async Task<ProfilDto> GetProfilBy(string email)
        {
            ProfilDto? response =new ();
            if (_httpClient is not null)
            {
                try
                {
                    response = await _httpClient.GetFromJsonAsync<ProfilDto>($"api/Profil/{email}");
                }
                catch(Exception)
                { }
                if (response is object)
                    return response;                
            }
            return new ProfilDto();
        }

        public async Task<Guid> GetUserIdBy(string email)
        {
            Guid response = Guid.Empty;
            if (_httpClient is not null)
            {
                try
                {
                    response = await _httpClient.GetFromJsonAsync<Guid>($"api/Profil/userid/{email}");
                }
                catch (Exception)
                { }
            }
            return response;
        }

        public async  Task<ControllerResponse> UpdateProfil(ProfilDto profil)
        {
            ControllerResponse defaultResponse = new ();
            if (_httpClient is null)
            {
                defaultResponse.ClearAndAddError("A profil frissítés nem lehetséges!");
            }
            else
            {
                try
                {
                    HttpResponseMessage httpResponse = await _httpClient.PostAsJsonAsync("api/Profil/", profil);
                    
                    string content = await httpResponse.Content.ReadAsStringAsync();
                    ControllerResponse? response= JsonConvert.DeserializeObject<ControllerResponse>(content);
                    if (response is not null)
                        return response;
                }
                catch(Exception ex)
                {
                    LibraryLogging.LoggingBroker.LogError($"{ex.Message}");
                }
            }
            defaultResponse.ClearAndAddError("A profil frissítés nem lehetséges!");
            return defaultResponse;

        }
    }
}
