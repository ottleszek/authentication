﻿using Authentication.Shared.Models;
using AuthenticationLibrary.Provider;
using AuthenticationLibrary.Services.Accounts;
using AuthenticationLibrary.Services.Token;
using AuthenticationLibrary.ViewModels.Accounts;
using AuthenticationLibrary.ViewModels.Login;
using Blazored.LocalStorage;
using LibraryClientServiceTemplate.ApiServices;
using LibraryClientServiceTemplate.ModelBrokerConnectors;
using LibraryClientServiceTemplate.ViewModelsTemplate;
using LibraryDataBrokerProject;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationLibrary.Extensions
{
    public static class AuthrizationClientExtensions
    {
        public static void ConfigureAuthenticationViewModles(this IServiceCollection services)
        {
            // ViewModel
            services.AddScoped<IRegistrationViewModel, RegistrationViewModel>();
            services.AddScoped<ILoginViewModel, LoginViewModel>();
        }

        public static void ConfigureAuthenticationServices(this IServiceCollection services)
        {
            // Services
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ILoginTokenStore, LoginTokenStore>();
            services.AddScoped<INofifyAuthenticationService, NofifyAuthenticationService>();
            // User managment
            services.AddScoped<IListViewModel<User>, ListViewModel<User>>();
            services.AddScoped<IListModelBrokerConnector<User>, ListModelBrokerConnector<User>>();
            // User role managment
            services.AddScoped<IListViewModel<UserRole>, ListViewModel<UserRole>>();
            services.AddScoped<IListModelBrokerConnector<UserRole>, ListModelBrokerConnector<UserRole>>();
            // Data broker
            services.AddScoped<IListDataBroker, ListApiService>();

        }

        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            services.AddHttpClient("AuthenticationApi", options =>
            {
                options.BaseAddress = new Uri("https://localhost:7220/");
            }).AddHttpMessageHandler<CustomHttpHandler>();

            // Local storage
            services.AddBlazoredLocalStorage();

            // Authorization
            services.AddAuthorizationCore();

            // Authentication State Provider
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddScoped<IUserIdentificaitonProvider,UserIdentificaitonProvider>();

            // Custom http handlrer
            services.AddScoped<CustomHttpHandler>();
        }
    }
}