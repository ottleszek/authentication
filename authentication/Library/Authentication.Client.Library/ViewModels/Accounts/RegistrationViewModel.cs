﻿using Authentication.Shared.Dtos;
using Authentication.Shared.Services.Accounts;
using CommunityToolkit.Mvvm.ComponentModel;
using LibraryCore.Errors;
using LibraryMvvm.Base;

namespace Authentication.Client.Library.ViewModels.Accounts
{
    public partial class RegistrationViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _firstName =  string.Empty;
        [ObservableProperty]
        private string _lastName = string.Empty;
        [ObservableProperty]
        private string _email = string.Empty;
        [ObservableProperty]
        private string _password = string.Empty;
        [ObservableProperty]
        private string _confirmPassword  = string.Empty;

        private IRegistrationService? _registrationService;

        [ObservableProperty]
        private ErrorStore _errorString = new();

        public RegistrationViewModel(IRegistrationService service)
        {
            _registrationService = service;
        }

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
                    ErrorString.ClearAndAddError(authenticationResponse.Error);
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
