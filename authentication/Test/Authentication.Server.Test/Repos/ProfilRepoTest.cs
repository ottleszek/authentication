using Authentication.Server.Context;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Server.Test.Repos
{
    public class ProfilRepoTest : IDisposable
    {
        public static DbContextOptions<AuthenticationContext> contextOptions = new DbContextOptionsBuilder<AuthenticationContext>()
                .UseInMemoryDatabase(databaseName: "AuthenticationTest" + Guid.NewGuid().ToString())
                .Options;

        private AuthenticationContext authenticationContext = new AuthenticationContext(contextOptions);

        public void Dispose()
        {
            authenticationContext.Dispose();
        }
    }
}
