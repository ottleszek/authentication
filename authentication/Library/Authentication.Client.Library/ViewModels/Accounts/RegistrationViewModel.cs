﻿using Authentication.Shared.Dtos;
using Authentication.Shared.Services.Accounts;
using CommunityToolkit.Mvvm.ComponentModel;
using LibraryBlazorMvvm.ViewModels;
using LibraryCore.Errors;

namespace Authentication.Client.Library.ViewModels.Accounts
{
    public partial class RegistrationViewModel : MvvmViewModelBase
    {
        private IRegistrationService? _registrationService;              

        public RegistrationViewModel(IRegistrationService service)
        {
            _registrationService = service;
        }

        [ObservableProperty]
        private string _firstName = string.Empty;
        [ObservableProperty]
        private string _lastName = string.Empty;
        [ObservableProperty]
        private string _email = string.Empty;
        [ObservableProperty]
        private string _password = string.Empty;
        [ObservableProperty]
        private string _confirmPassword = string.Empty;
        [ObservableProperty]
        private ErrorStore _errorString = new();

        public async Task<bool> UserRegistrationAsync()
        {
            if (_registrationService is not null)
            {
                AuthenticationResponseDto authenticationResponse = new();
                try
                {
                    authenticationResponse = await _registrationService.UserRgistration(CopyToDto());
                }
                catch (Exception ex)
                {
                    LibraryLogging.LoggingBroker.LogError(ex.Message);
                    ErrorString.ClearAndAddError("Regisztráció nem lehetséges");
                }

                if (authenticationResponse.HasError)
                {
                    ErrorString.ClearAndAddError(authenticationResponse.Message);
                }
                else
                {
                    ErrorString.ClearErrorStore();
                    return true;
                }
            }
            return false;
        }

        private UserRegistrationDto CopyToDto()
        {
            return new UserRegistrationDto
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password
            };
        }

    }
}
