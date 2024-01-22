using Authentication.Server.Context;
using Authentication.Server.Repos;
using Authentication.Server.Repos.DataBroker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit.Microsoft.DependencyInjection;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace Authentication.Server.Test.Fixtures
{
    public class ProfilRepoInMemoryDataBrokerFixture : TestBedFixture
    {
        protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
            => services
            .AddTransient<IProfilRepo,ProfilRepoInMemoryDataBroker>()
            .AddTransient<IDbContextFactory<AuthenticationInMemoryContext>>()
            .AddTransient<AuthenticationInMemoryContext>()
            .Configure<Options>(config => configuration?.GetSection("Options").Bind(config));


        protected override ValueTask DisposeAsyncCore()
            => new();

        protected override IEnumerable<TestAppSettings> GetTestAppSettings()
        {
            yield return new() { Filename = "appsettings.json", IsOptional = false };
        }
    }
}
