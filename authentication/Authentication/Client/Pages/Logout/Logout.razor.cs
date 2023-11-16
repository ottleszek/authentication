using AuthenticationLibrary.Services.Accounts;
using Microsoft.AspNetCore.Components;

namespace Authentication.Client.Pages.Logout
{
    public partial class Logout
    {
        [Inject] IHttpClientFactory? HttpClientFactory { get; set; }
        [Inject] IAuthenticationService? AuthenticationService { get; set; }
        [Inject] NavigationManager? NavigationManager { get; set; }

        private HttpClient? _httpClient;

        protected override Task OnInitializedAsync()
        {
            if (HttpClientFactory is not null && AuthenticationService is not null && NavigationManager is not null)
            {
                _httpClient = HttpClientFactory.CreateClient("AuthenticationApi");
                AuthenticationService.Logout();
                _httpClient.DefaultRequestHeaders.Authorization = null;
                NavigationManager.NavigateTo("/");
            }
            return base.OnInitializedAsync();
        }
    }
}
