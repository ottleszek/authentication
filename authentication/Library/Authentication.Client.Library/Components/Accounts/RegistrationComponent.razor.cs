﻿using Authentication.Client.Library.Validation;
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
        private MudForm _form = new();

        [CascadingParameter] public Task<AuthenticationState>? AuthenticationState { get; set; }
        [Inject] private NavigationManager? NavigationManager { get; set; }
        
        [Inject] private HttpClient? HttpClient { get; set; }

        [Inject] private RegistrationValidation? Validation {get; set;}

        protected override Task OnParametersSetAsync()
        {
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
            else
            {
                ViewModel.Email = string.Empty;
                ViewModel.Password = string.Empty;                    
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
