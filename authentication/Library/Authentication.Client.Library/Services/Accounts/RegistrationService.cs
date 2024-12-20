﻿using Authentication.Shared.Dtos;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Authentication.Shared.Services.Accounts
{
    public class RegistrationService : IRegistrationService
    {
        private HttpClient _httpClient;

        public RegistrationService(IHttpClientFactory httpClientFactory)

        {
            _httpClient = httpClientFactory.CreateClient("AuthenticationApi");
        }

        public async Task<AuthenticationResponseDto> UserRgistration(UserRegistrationDto user)
        {

            AuthenticationResponseDto? authenticationResponse = new();

            if (_httpClient is null)
            {
                authenticationResponse.ClearAndAddError("Authentikáció nem lehetséges!");
            }
            else
            {
                try
                {
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/Registration/register", user);

                    string content = await response.Content.ReadAsStringAsync();
                    authenticationResponse = JsonConvert.DeserializeObject<AuthenticationResponseDto>(content);
                    if (authenticationResponse is not null)                    
                        return authenticationResponse;                    
                }
                catch (Exception ex)
                {
                    LibraryLogging.LoggingBroker.LogError($"{ex.Message}");
                }
            }
            authenticationResponse ??= new AuthenticationResponseDto();
            authenticationResponse.ClearAndAddError("A felhasználói adatok elérése nem lehetséges!");
            return authenticationResponse;
        }        
    }
}
