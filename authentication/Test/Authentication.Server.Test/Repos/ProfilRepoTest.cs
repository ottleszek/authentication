using Authentication.Server.Context;
using Authentication.Server.Repos;
using Authentication.Server.Repos.DataBroker;
using Authentication.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Server.Test.Repos
{
    public class ProfilRepoTest : IDisposable
    {
        // https://umplify.github.io/xunit-dependency-injection/
        // https://github.com/Umplify/xunit-dependency-injection/tree/main/examples/Xunit.Microsoft.DependencyInjection.ExampleTests/Fixtures

        /*public static DbContextOptions<AuthenticationContext> contextOptions = new DbContextOptionsBuilder<AuthenticationContext>()
                .UseInMemoryDatabase(databaseName: "AuthenticationTest" + Guid.NewGuid().ToString())
                .Options;

        private AuthenticationInMemoryContext authenticationContext = new AuthenticationInMemoryContext(contextOptions);*/
        private IProfilRepo? _profilRepo = null;

        public ProfilRepoTest(IProfilRepo? profilRepo)
        {
            _profilRepo = profilRepo;
        }

        public class Startup
        {
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddTransient<IProfilRepo, ProfilRepoInMemoryDataBroker>();
            }
        }


        public void Dispose()
        {
            //authenticationContext.Dispose();
        }

        [Fact]
        //[InlineData("admin@teszt.hu")]
        public async Task GetByEmail()
        {
            if (_profilRepo is not null)
            {
                User? user = await _profilRepo.GetUserBy("admin@teszt.hu");
            }
        }
    }
}
