using Authentication.Server.Context;
using Authentication.Server.Repos;
using Authentication.Server.Repos.DataBroker;
using Authentication.Server.Test.Fixtures;
using Authentication.Shared.Models;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace Authentication.Server.Test.Repos
{
    public class ProfilRepoTest : TestBed<ProfilRepoInMemoryDataBrokerFixture>, IDisposable
    {
        // https://umplify.github.io/xunit-dependency-injection/
        // https://github.com/Umplify/xunit-dependency-injection/tree/main/examples/Xunit.Microsoft.DependencyInjection.ExampleTests/Fixtures

        /*public static DbContextOptions<AuthenticationContext> contextOptions = new DbContextOptionsBuilder<AuthenticationContext>()
                .UseInMemoryDatabase(databaseName: "AuthenticationTest" + Guid.NewGuid().ToString())
                .Options;

        private AuthenticationInMemoryContext authenticationContext = new AuthenticationInMemoryContext(contextOptions);*/

        //private readonly Options _options;
        private IProfilRepo? _profilRepo = null;


        public ProfilRepoTest(ITestOutputHelper testOutputHelper,ProfilRepoInMemoryDataBrokerFixture fixture) : base(testOutputHelper,fixture)
        { }


        /*
        public void Dispose()
        {
            //authenticationContext.Dispose();
        }
        */

        [Fact]
        //[InlineData("admin@teszt.hu")]
        public async Task GetByEmail()
        {
            try
            {
                IProfilRepo profilRepo = _fixture.GetScopedService<IProfilRepo>(_testOutputHelper)!;
                if (_profilRepo is not null)
                {
                    User? user = await _profilRepo.GetUserBy("admin@teszt.hu");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
