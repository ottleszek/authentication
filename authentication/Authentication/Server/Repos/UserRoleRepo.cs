using Authentication.Shared.Models;
using LibaryDatabase.Model;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Server.Repos
{
    public class UserRoleRepo<TDbContext> : IUserRoleRepo where TDbContext : DbContext
    {
        private readonly DbSet<UserRole>? _userRoleSet;
        private readonly TDbContext _dbContext;

        public UserRoleRepo(IDbContextFactory<TDbContext> dbContextFactory)
        {

            TDbContext dbContext = dbContextFactory.CreateDbContext();
            _dbContext = dbContext;

            try
            {
                _userRoleSet = dbContext.GetDbSet<UserRole>();
            }
            catch
            {
                LibraryLogging.LoggingBroker.LogError($"{nameof(UserRole)} táblát nem lehet elérni.");
            }
        }

        public string GetEnglishNameBy(Guid userRoleId)
        {
            string? result = string.Empty;
            if (_userRoleSet is not null)
            {
                result = _userRoleSet.Where(role => role.Id == userRoleId).Select(role => role.EnglishName).FirstOrDefault();
            }
            return string.IsNullOrEmpty(result) ? string.Empty : result;
        }

        public Guid? GetByEnglishName(string englishName)
        {
            if (_userRoleSet is not null)
            {
                return _userRoleSet.Where(role => role.EnglishName == englishName).Select(role => role.Id).FirstOrDefault();
            }
            return Guid.Empty;
        }
    }
}
