using Authentication.Shared.Models;
using Microsoft.EntityFrameworkCore;
using LibraryDatabase.Model;

namespace Authentication.Server.Repos.Base
{
    public abstract class UserGetRepoBase<TDbContext> : IIUserGetRepoBase where TDbContext : DbContext
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

        public async Task<bool> IsUserExsist(string email)
        {
            if (_userSet is not null)
            {
                if (await _userSet.AnyAsync(user => user.Email.ToLower() == email.ToLower()))
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<User?> GetUserBy(string email)
        {
            if (_userSet is not null)
            {
                return await _userSet.Where(user => user.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<Guid?> GetIdBy(string email)
        {
            if (_userSet is not null)
            {
                User? user = await _userSet.Where(user => user.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
                if (user is not null)
                    return user.Id;
            }
            return null;
        }
    }
}
