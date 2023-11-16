using AuthenticationLibrary.Services.Token;
using AuthenticationLibrary.Shared.Dtos;
using AuthenticationLibrary.ViewModels;
using Blazored.LocalStorage;
using LibraryCore.Errors;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Security.Claims;

namespace AuthenticationLibrary.Provider
{
    public class CustomHttpHandler : DelegatingHandler
    {

        private readonly ILocalStorageService? _localStorageService;
        private readonly IHttpClientFactory?  _httpClientFactory;
        private readonly ILoginTokenStore? _loginTokenStore;

        public CustomHttpHandler(ILocalStorageService localStorageService, IHttpClientFactory httpClientFactory, ILoginTokenStore? accessAndRefreshTokenService)
        {
            _localStorageService = localStorageService;
            _httpClientFactory = httpClientFactory;
            _loginTokenStore = accessAndRefreshTokenService;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri is not null && _localStorageService is not null)
            {
                if (request.RequestUri.AbsolutePath.ToLower().Contains("login") ||
                    request.RequestUri.AbsolutePath.ToLower().Contains("register") ||
                    request.RequestUri.AbsolutePath.ToLower().Contains("refresh-token")
                    )
                {
                    return await base.SendAsync(request, cancellationToken);
                }

                var jwtToken = await _localStorageService.GetItemAsync<string>("jwt-access-token");
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    request.Headers.Add("Authorization", $"Bearer {jwtToken}");
                }

                HttpResponseMessage orginalResponse = await base.SendAsync(request, cancellationToken);

                if (orginalResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return await InvokeRefreshApiCall(orginalResponse, request, cancellationToken, jwtToken);
                }
                return orginalResponse;
            }

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            httpResponseMessage.StatusCode = System.Net.HttpStatusCode.BadRequest;
            return httpResponseMessage;
        }

        private async Task<HttpResponseMessage> InvokeRefreshApiCall(HttpResponseMessage originalResponse, HttpRequestMessage orginalRequest, CancellationToken cancellationToken, string jwtToken)
        {
            if (_httpClientFactory is not null && _localStorageService is not null)
            {
                string refreshToken = await _localStorageService.GetItemAsync<string>("refresh-token");
                List<Claim> claims = (List<Claim>)JwtParser.ParseClaimsFromJwt(jwtToken);

                Guid userId = claims.Where(claim => claim.Type == "Id").Select(claim => Guid.Parse(claim.Value)).FirstOrDefault();
                RefreshTokenDto refreshTokenDto = new RefreshTokenDto
                {
                    Token = refreshToken,
                    UserId = userId
                };

                HttpClient _httpClient = _httpClientFactory.CreateClient("AuthenticationApi");
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/RefreshToken/refresh-token", refreshTokenDto);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    TokenResponseDto? regeneretedTokens = JsonConvert.DeserializeObject<TokenResponseDto>(content);
                    if (regeneretedTokens is not null && _loginTokenStore is not null)
                    {
                        ErrorStore error = await _loginTokenStore.SaveAccessTokenAndRefreshToken(regeneretedTokens.AccessToken, regeneretedTokens.RefreshToken);

                        orginalRequest.Headers.Remove("Authorization");
                        orginalRequest.Headers.Add("Authorization", $"Bearer {regeneretedTokens.AccessToken}");

                        // ReSend orginal request
                        return await base.SendAsync(orginalRequest, cancellationToken);
                    }                   
                }
            }
            return originalResponse;
        }
    }
}
