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

        public async  Task<ControllerResponse> UpdateProfil(ProfilDto profil)
        {
            ControllerResponse? response = new ();
            if (_httpClient is null)
            {
                response.ClearAndAddError("A profil frissítés nem lehetséges!");
            }
            else
            {
                try
                {
                    HttpResponseMessage httpResponse = await _httpClient.PostAsJsonAsync("api/Profil/", profil);
                    
                    string content = await httpResponse.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<ControllerResponse>(content);
                    if (response is not null)
                        return response;
                }
                catch(Exception ex)
                {
                    LibraryLogging.LoggingBroker.LogError($"{ex.Message}");
                }
            }
            response ??= new ControllerResponse();
            response.ClearAndAddError("A profil frissítés nem lehetséges!");
            return response;

        }
    }
}
