using Authentication.Client;
using Authentication.Client.Library.Extensions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using MudBlazor.Services;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Globalization;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// ViewModels
builder.Services.ConfigureAuthenticationViewModles();

// Services
builder.Services.ConfigureAuthenticationServices();

// Authentication
builder.Services.ConfigureAuthentication();

// MudBlazor
builder.Services.AddMudServices();


WebAssemblyHost app = builder.Build();

//await builder.Build().RunAsync();

await app.RunAsync();
