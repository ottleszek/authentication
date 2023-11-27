using Authentication.Client.Library.ViewModels.Accounts;
using Authentication.Shared.Dtos;
using Authentication.Shared.Services.Accounts;
using LibraryCore.Errors;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace Authentication.Client.Pages.Accounts
{
    public partial class Registration
    {
        [CascadingParameter] public Task<AuthenticationState>? AuthenticationState { get; set; }
        [Inject] private NavigationManager? NavigationManager {  get; set; }
        [Inject] private IRegistrationService? RegistrationService { get; set; }

        [Inject] private HttpClient? HttpClient { get; set; }

        [Inject] private IRegistrationViewModel? RegistrationViewModel { get; set; }
        
        private RegistrationValidation? _validation;

        private ErrorStore ErrorString = new();
        private MudForm _form=new();

        protected override Task OnParametersSetAsync()
        {
            if (HttpClient is not null)
                _validation = new RegistrationValidation(HttpClient);
            return base.OnParametersSetAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            if (AuthenticationState is not null && NavigationManager is not null)
            {
                var user = (await AuthenticationState).User;
                if (user is not null && user.Identity is not null)
                {
                    if (user.Identity.IsAuthenticated)
                        NavigationManager.NavigateTo("/");
                }
            }
            await base.OnInitializedAsync();
        }

        private async Task RegisterAsync()
        {
            await _form.Validate();
            if (RegistrationService is not null && RegistrationViewModel is not null &&_form.IsValid)
            {
                UserRegistrationDto userRegistrationDto = RegistrationViewModel.ConverToUserRegistrationDto;

                AuthenticationResponseDto authenticationResponse =new();
                try
                {
                    authenticationResponse = await RegistrationService.UserRgistration(userRegistrationDto);
                }
                catch (Exception ex)
                {
                    LibraryLogging.LoggingBroker.LogError(ex.Message);
                    ErrorString.ClearAndAddError("Regisztráció nem lehetséges");                    
                }

                if (authenticationResponse.HasError)
                {
                    ErrorString.ClearAndAddError(authenticationResponse.Error);
                }
                else
                {
                    ErrorString.ClearErrorStore();
                    //LibraryLogging.LoggingBroker.LogInformation("Sikeres regisztráció.");
                    NavigationManager?.NavigateTo("/registration-confirmation");
                }            
            }
        }
    }
}
