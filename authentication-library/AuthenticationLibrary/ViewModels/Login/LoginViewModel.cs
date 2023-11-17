﻿using AuthenticationLibrary.Shared.Dtos;

namespace AuthenticationLibrary.ViewModels.Login
{
    public class LoginViewModel : ILoginViewModel
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public UserLoginDto ConvertToUserLoginDto => new UserLoginDto
        {
            Email = Email,
            Password = Password
        };

    }
}