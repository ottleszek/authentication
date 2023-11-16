using Authentication.Client;
using AuthenticationLibrary.Extensions;
using AuthenticationLibrary.Provider;
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

var levelSwitch = new LoggingLevelSwitch();
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.ControlledBy(levelSwitch)
    .Enrich.WithProperty("InstanceId", Guid.NewGuid().ToString("n"))
    .Enrich.FromLogContext()
    .WriteTo.BrowserHttp(controlLevelSwitch: levelSwitch)
    .WriteTo.BrowserConsole
    (
        restrictedToMinimumLevel: LogEventLevel.Information,
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
        CultureInfo.InvariantCulture,
        jsRuntime: app.Services.GetRequiredService<IJSRuntime>()
    )
    .WriteTo.File("d:/logs/client/mylog.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

Log.Information("Serilog elindult.");
Log.Information("Authentication kliens elindult.");

await app.RunAsync();
