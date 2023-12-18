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
            services.AddScoped<ProfilValidation>();
            services.AddScoped<UserValidation>();
            // ViewModels
            services.AddScoped<LoginViewModel>();
            services.AddScoped<ProfilViewModel>();
            services.AddScoped<RegistrationViewModel>();

            // User managment
            services.AddScoped<IListViewModel<User>, ListViewModel<User>>();
            services.AddScoped<IListAndDeleteViewModel<User>, ListAndDeleteViewModel<User>>();

            services.AddScoped<MvvmCrudViewModelBase<User>>();   
        }

        public static void ConfigureAuthenticationServices(this IServiceCollection services)
        {
            // Services
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ILoginTokenStore, LoginTokenStore>();
            services.AddScoped<INofifyAuthenticationService, NofifyAuthenticationService>();

            services.AddScoped<IProfilService, ProfilService>();
            
            // User role managment
            services.AddScoped<IListViewModel<UserRole>, ListViewModel<UserRole>>();
            services.AddScoped<IListBrokerConnector<UserRole>, ListBrokerConnector<UserRole>>();
            // Data broker
            services.AddScoped<IListDataBroker, ListHttpService>();
            services.AddScoped<IGetDataBroker, GetHttpService>();
            services.AddScoped<IUpdateDataBroker, UpdateHttpService>();
            services.AddScoped<ICrudDataBroker, CrudHttpService>();
            services.AddScoped<IListAndDeleteDataBroker, ListAndDeleteHttpService>();
            //Broker connectors
            services.AddScoped<IListBrokerConnector<User>, ListBrokerConnector<User>>();
            services.AddScoped<IUpdateBrokerConnector<User>, UpdateBrokerConnector<User>>();
            services.AddScoped<IGetBrokerConnector<User>, GetBrokerConnector<User>>();
            services.AddScoped<IListBrokerConnector<User>, ListBrokerConnector<User>>();
            services.AddScoped<ICrudBrokerConnector<User>,CrudBrokerConnectorr<User>>();
            services.AddScoped<IListAndDeleteBrokerConnector<User>, ListAndDeleteBrokerConnector<User>>();    

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
