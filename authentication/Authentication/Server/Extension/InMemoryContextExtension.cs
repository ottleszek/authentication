using Authentication.Server.Context;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Server.Extension
{
    public static class InMemoryContextExtension
    {
        public static void ConfigureInMemoryContext(this IServiceCollection services)
        {
            string dbName = "Authentication" + Guid.NewGuid();
            services.AddDbContextFactory<AuthenticationContext>(
                options => options.UseInMemoryDatabase(databaseName: dbName)
                );
            services.AddDbContextFactory<AuthenticationInMemoryContext>(
                options => options.UseInMemoryDatabase(databaseName: dbName)
                );
       }
    }
}
