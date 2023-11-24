using Authentication.Server.Datas.Entities;
using LibraryDatabase.Model;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Server.Repos
{
    public class UserIdentificationRepo<TDbContext> : IUserIdentificationRepo where TDbContext : DbContext
    {
        private readonly DbSet<UserIdentification>? _userIdentificationsSet;
        private readonly TDbContext _dbContext;

        public UserIdentificationRepo(IDbContextFactory<TDbContext> dbContextFactory)
        {

            TDbContext dbContext = dbContextFactory.CreateDbContext();
            _dbContext = dbContext;

            try
            {
                _userIdentificationsSet = dbContext.GetDbSet<UserIdentification>();
            }
            catch
            {
                LibraryLogging.LoggingBroker.LogError($"{nameof(_userIdentificationsSet)} táblát nem lehet elérni.");
            }
        }

        public string? GetPassword(Guid id)
        {
            string password = string.Empty;
            if (_userIdentificationsSet is not null)
            {
                return _userIdentificationsSet.Where(identification => identification.Id == id).Select(identification => identification.Password).FirstOrDefault();
            }
            return password;
        }

        public Task<RepositoryResponse> SaveNewUserPassword(Guid userId, string password)
        {
            UserIdentification newUserIdentification = new UserIdentification
            {
                Id = userId,
                Password = password
            };
            return RegisterNewUser(newUserIdentification);
        }

        public async Task<RepositoryResponse> RegisterNewUser(UserIdentification userIdentification)
        {
            RepositoryResponse response = new RepositoryResponse();
            try
            {
                if (_userIdentificationsSet is not null)
                {
                    _userIdentificationsSet.Add(userIdentification);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                response.ClearAndAddError($"{nameof(UserIdentificationRepo<TDbContext>)}\nSQL lekérdezés nem hajtható végre!\n{ex.Message}");
            }
            return response;
        }


    }
}
