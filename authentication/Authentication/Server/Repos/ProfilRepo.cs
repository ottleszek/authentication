using Authentication.Server.Repos.Base;
using Authentication.Shared.Models;
using LibraryDatabase.Model;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Server.Repos
{
    public class ProfilRepo<TDbContext> : UserGetRepoBase<TDbContext>, IProfilRepo where TDbContext : DbContext
    {
        private readonly DbSet<User>? _userSet;
        private readonly TDbContext _dbContext;

        public ProfilRepo(IDbContextFactory<TDbContext> dbContextFactory)
            : base(dbContextFactory)
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

        public async Task<string> GetProfilImageTimeStamp(string email)
        {
            string profilImageTimeStamp = string.Empty;
            if (_userSet is not null)
            {
                User? user = await _userSet.Where(user => user.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
                if (user is not null)
                {
                    return user.ProfilImageTimeStamp;
                }
            }
            return string.Empty;
        }

        public async Task<RepositoryResponse> UpdateProfil(User user)
        {
            RepositoryResponse response = new();

            _dbContext.ChangeTracker.Clear();
            _dbContext.Entry(user).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                response.AppendNewError(e.Message);
            }
            return response;
        }

        public async Task<RepositoryResponse> UpdateProfilImageTimeStamp(string email, string profilImageTimeStamp)
        {
            RepositoryResponse response = new();
            User user;
            try
            {
                User? foundedUser = await GetUserBy(email);
                if (foundedUser is null)
                {
                    response.AppendNewError("A felhasználó nem létezik.");
                    return response;
                }
                else
                {
                    user = foundedUser;
                    user.ProfilImageTimeStamp = profilImageTimeStamp;
                }
            }
            catch (Exception e)
            {
                response.AppendNewError(e.Message);
                return response;
            }

            try
            {
                _dbContext.ChangeTracker.Clear();
                _dbContext.Update(user);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                response.AppendNewError(e.Message);
            }
            return response;
        }
    }
}
