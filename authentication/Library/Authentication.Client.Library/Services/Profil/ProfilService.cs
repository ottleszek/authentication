using Authentication.Shared.Dtos;
using Authentication.Shared.Model;
using LibraryClientServiceTemplate.HttpServices;
using LibraryCore.Responses;
using LibraryLogging;
using Newtonsoft.Json;
using System;
using System.Net;
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

        // Profil
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
                    response = await _httpClient.GetFromJsonAsync<Guid>($"api/Profil/get-user-id/{email}");
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

                    if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                    {
                        string content = await httpResponse.Content.ReadAsStringAsync();
                        ControllerResponse? response = JsonConvert.DeserializeObject<ControllerResponse>(content);
                        if (response is null)
                        {
                            return new ControllerResponse("A profil frissítése http kérés hibát okozott!");
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
                    LoggingBroker.LogError($"{ex.Message}");
                }
            }
            defaultResponse.ClearAndAddError("A profil frissítés nem lehetséges!");
            return defaultResponse;

        }

        // Profil Image
        public async Task<bool> IsProfileImageExist(ProfilImageFileName profilImageFileName)
        {
            if (_httpClient is null)
                return false;
            else
            {
                try
                {
                    HttpResponseMessage httpResponse = await _httpClient.PostAsJsonAsync("api/ProfilImage/is-profil-image-exsist", profilImageFileName.ToDto());
                    if (httpResponse.IsSuccessStatusCode || httpResponse.StatusCode == HttpStatusCode.BadRequest)
                    {
                        string content = await httpResponse.Content.ReadAsStringAsync();
                        bool result = JsonConvert.DeserializeObject<bool>(content);
                        return result;
                    }
                    else
                    {
                        httpResponse.EnsureSuccessStatusCode();
                    }

                }
                catch (Exception ex)
                {
                    LoggingBroker.LogError(nameof(ProfilService), nameof(IsProfileImageExist), ex.Message);
                }
            }
            return false;
        }

        public async Task<ControllerResponse> DeleteProfilImage(ProfilImageFileName profilImageFileName)
        {
            ControllerResponse defaultResponse = new();
            if (_httpClient is object)
            {
                try
                {
                    HttpResponseMessage httpResponse = await _httpClient.PostAsJsonAsync("api/ProfilImage/delete-profil", profilImageFileName.ToDto());
                    if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                    {
                        string content = await httpResponse.Content.ReadAsStringAsync();
                        ControllerResponse? response = JsonConvert.DeserializeObject<ControllerResponse>(content);
                        if (response is null)
                        {
                            return new ControllerResponse("A profil kép törlése http kérés hibát okozott!");
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
                catch (Exception ex)
                {
                    LoggingBroker.LogError(nameof(CrudHttpService), nameof(DeleteProfilImage), ex.Message);
                }
            }
            defaultResponse.ClearAndAddError("Profil kép törlése nem lehetséges!");
            return defaultResponse;
        }

        public async Task<ControllerResponse> ProfilImageTimeStampUpdate(string email, string profilImageTimeStamp)
        {
            ControllerResponse defaultResponse = new();
            if (_httpClient is null)
            {
                defaultResponse.ClearAndAddError("A profil kép frissítés nem lehetséges!");
            }
            else
            {
                try
                {
                    ProfilImageTimeStampUpdateDto profilImageTimeStampUpdateDto = new ProfilImageTimeStampUpdateDto
                    {
                        Email = email,
                        ProfilImageTimeStamp = profilImageTimeStamp
                    };
                    HttpResponseMessage httpResponse = await _httpClient.PostAsJsonAsync("api/Profil/profil-image-update", profilImageTimeStampUpdateDto);

                    if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                    {
                        string content = await httpResponse.Content.ReadAsStringAsync();
                        ControllerResponse? response = JsonConvert.DeserializeObject<ControllerResponse>(content);
                        if (response is null)
                        {
                            return new ControllerResponse("A profil kép frissítése http kérés hibát okozott!");
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
                catch (Exception ex)
                {
                    LoggingBroker.LogError($"{ex.Message}");
                }
            }
            defaultResponse.ClearAndAddError("A profil kép frissítés nem lehetséges!");
            return defaultResponse;
        }
    }
}
