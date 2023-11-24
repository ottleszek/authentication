using Authentication.Shared.Models;
using LibraryDatabase.Model;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Server.Repos.Base
{
    public class UserGetRepoBase<TDbContext> : IIUserGetRepoBase where TDbContext : DbContext
    {
        private readonly DbSet<User>? _userSet;
        private readonly TDbContext _dbContext;

        public UserGetRepoBase(IDbContextFactory<TDbContext> dbContextFactory)
        {
            TDbContext dbContext = dbContextFactory.CreateDbContext();
            _dbContext = dbContext;

            try
            {
                _userSet = dbContext.GetDbSet<User>();
            }
            catch
            {
                LibraryLogging.LoggingBroker.LogError($"{nameof(User)} táblát nem lehet elérni.");
            }
        }

        public bool IsUserExsist(string email)
        {
            if (_userSet is not null)
            {
                if (_userSet.Any(user => user.Email.ToLower() == email.ToLower()))
                    return true;
                else
                    return false;
            }
            return false;
        }

        public User? GetUserBy(string email)
        {
            if (_userSet is not null)
            {
                return _userSet.Where(user => user.Email.ToLower() == email.ToLower()).FirstOrDefault();
            }
            return null;
        }
    }
}
