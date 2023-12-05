using Authentication.Client.Library.Validation;
using Authentication.Client.Library.ViewModels.Accounts;
using Authentication.Shared.Dtos;
using LibraryBlazorMvvm.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Authentication.Client.Library.Components
{
    public partial class RegistrationComponent : MvvmComponentBase<RegistrationViewModel>
    {
        [CascadingParameter] public Task<AuthenticationState>? AuthenticationState { get; set; }
        [Inject] private NavigationManager? NavigationManager { get; set; }
        

        [Inject] private HttpClient? HttpClient { get; set; }

        //[Inject] private IRegistrationViewModel? RegistrationViewModel { get; set; }

        private RegistrationValidation? _validation;

        private MudForm _form = new();

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
            if (ViewModel is not null && _form.IsValid)
            {
                AuthenticationResponseDto authenticationResponse = new();
                bool registrationSucces = await ViewModel.UserRegistrationAsync();
                if (registrationSucces)
                {
                    NavigationManager?.NavigateTo("/registration-confirmation");
                }

            }
        }
    }
}
