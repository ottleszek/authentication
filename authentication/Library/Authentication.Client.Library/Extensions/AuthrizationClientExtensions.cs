using Authentication.Client.Library.Services.Profil;
using Authentication.Client.Library.Validation;
using Authentication.Client.Library.ViewModels.Accounts;
using Authentication.Client.Library.ViewModels.Login;
using Authentication.Client.Library.ViewModels.User;
using Authentication.Shared.Models;
using Authentication.Shared.Services.Accounts;
using Authentication.Shared.Services.Token;
using AuthenticationLibrary.LocalStorage;
using AuthenticationLibrary.Provider;
using Blazored.LocalStorage;
using LibraryBlazorMvvm.ViewModels;
using LibraryClientServiceTemplate.HttpServices;
using LibraryClientServiceTemplate.ModelBrokerConnectors;
using LibraryClientServiceTemplate.ViewModelsTemplate;
using LibraryDataBroker;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Client.Library.Extensions
{
    public static class AuthrizationClientExtensions
    {
        public static void ConfigureAuthenticationViewModles(this IServiceCollection services)
        {
            // ViewModel
            //services.AddScoped<IRegistrationViewModel, RegistrationViewModel>();
            //services.AddScoped<ILoginViewModel, LoginViewModel>();
            //services.AddScoped<IProfilViewModel, ProfilViewModel>();

            // Validations
            services.AddScoped<RegistrationValidation>();
            // ViewModels
            services.AddScoped<LoginViewModel>();
            services.AddScoped<ProfilViewModel>();
            services.AddScoped<RegistrationViewModel>();

            services.AddScoped<MvvmItemViewModelBase<User>>();
        }

        public static void ConfigureAuthenticationServices(this IServiceCollection services)
        {
            // Services
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ILoginTokenStore, LoginTokenStore>();
            services.AddScoped<INofifyAuthenticationService, NofifyAuthenticationService>();

            services.AddScoped<IProfilService, ProfilService>();
            // User managment
            services.AddScoped<IListViewModel<User>, ListViewModel<User>>();
            services.AddScoped<IListBrokerConnector<User>, ListBrokerConnector<User>>();
            // User role managment
            services.AddScoped<IListViewModel<UserRole>, ListViewModel<UserRole>>();
            services.AddScoped<IListBrokerConnector<UserRole>, ListBrokerConnector<UserRole>>();
            // Data broker
            services.AddScoped<IListDataBroker, ListHttpService>();
            services.AddScoped<IGetDataBroker, GetHttpService>();


            services.AddScoped<IGetBrokerConnector<User>, GetBrokerConnector<User>>();
			services.AddScoped<IListBrokerConnector<User>, ListBrokerConnector<User>>();


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
