﻿using Authentication.Server.Context;
using Authentication.Server.Repos;
using Authentication.Server.Repos.DataBroker;
using Authentication.Server.Services;
using AuthenticationLibrary.Settings;
using LibraryDataBrokerProject;
using LibraryLogging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Authentication.Server.Extension
{
    public static class AuthenticationExtension
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            
            services.AddCors(option =>
                 option.AddPolicy(name: "AuthenticationCors",
                     policy =>
                     {
                         policy.WithOrigins("https://localhost:7220/")
                         .AllowAnyHeader()
                         .AllowAnyMethod();
                     }
                 )
            );    
           LoggingBroker.LogInformation("Cord policy hozzáadva!");
        }

        public static void ConfigureJwtSection(this IServiceCollection services, ConfigurationManager config)
        {
            services.Configure<TokenSettings>(config.GetSection(nameof(TokenSettings)));
            TokenSettings? tokenSettings = config.GetSection(nameof(TokenSettings)).Get<TokenSettings>();
            LoggingBroker.LogInformation("JWT szekció beolvasva!");

            if (tokenSettings is object)
            {
                services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenSettings.Issuer,
                        ValidAudience = tokenSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SecretKey)),

                        ClockSkew = TimeSpan.Zero
                    };
                });
                LoggingBroker.LogInformation("JWT regisztrálása megtörtént!");
            }
        }

        public static void ConfigureAuthenticationInMemoryRepos(this IServiceCollection services)
        {  
            services.AddSingleton<IAccountRepo, AccountInMemoryRepoDataBroker>();
            services.AddSingleton<IUserRefreshTokenRepo, UserRefreshTokenRepoDataBroker>();
            services.AddSingleton<IUserRoleRepo, UserRoleRepoInMemoryDataBroker>();
            services.AddSingleton<IUserIdentificationRepo, UserIdentificationRepoInMemoryDataBroker>();

            services.AddSingleton<IListDataBroker, ListInMemoryDataBroker>();





            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedEmail = true)
                .AddEntityFrameworkStores<AuthenticationInMemoryContext>();



            LoggingBroker.LogInformation("In memory repók létrehozva!");
        }

        public static void ConfigureAuthenticationServices(this IServiceCollection services)
        {
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ITokenService, TokenService>();
            
            LoggingBroker.LogInformation("Service-ek létrehozva!");
        }
    }
}