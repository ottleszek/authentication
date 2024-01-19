using Authentication.Server.Context;
using Authentication.Server.Extension;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Serilog;
using System.Text.Json.Serialization;
using Toolbelt.Extensions.DependencyInjection;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/server/mylog.txt",rollingInterval: RollingInterval.Day)
    .MinimumLevel.Debug()
    .CreateLogger();

Log.Information("Authentication server elindult.");

var builder = WebApplication.CreateBuilder(args);

//Swagger
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve); ;
builder.Services.AddRazorPages();

// Cors
builder.Services.ConfigureCors();

// JWT Section
builder.Services.ConfigureJwtSection(builder.Configuration);

// Context and database
builder.Services.ConfigureInMemoryContext();
builder.Services.ConfigureAuthenticationInMemoryRepos();

// Services
builder.Services.ConfigureAuthenticationServices();

var app = builder.Build();

// InMemory database data
using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AuthenticationInMemoryContext>();
    
    // InMemory test data
    dbContext.Database.EnsureCreated();

    // InMemory additional user test data
    UserManager<IdentityUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    userManager.AddIdentityUserTestData();

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCssLiveReload();
    app.UseWebAssemblyDebugging();   
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

// Cors
app.UseCors("AuthenticationCors");

// Autentikáció
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

//Static Files
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"staticfiles")),
    RequestPath = new PathString("/StaticFiles")
});

app.Run();
