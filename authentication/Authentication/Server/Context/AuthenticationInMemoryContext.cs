using Microsoft.EntityFrameworkCore;

namespace Authentication.Server.Context
{
    public class AuthenticationInMemoryContext : AuthenticationContext
    {
        public AuthenticationInMemoryContext(DbContextOptions<AuthenticationContext> options) : base(options)
        {
        }
    }
}
