using Authentication.Server.Datas.Entities;
using Authentication.Shared.Dtos;
using LibraryDatabase.Model;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Server.Repos
{
    public class UserRefreshTokenRepo<TDbContext> : IUserRefreshTokenRepo where TDbContext : DbContext
    {
        private readonly DbSet<UserRefreshToken>? _userRefreshRepo;
        private readonly TDbContext _dbContext;

        public UserRefreshTokenRepo(IDbContextFactory<TDbContext> dbContextFactory)
        {

            TDbContext dbContext = dbContextFactory.CreateDbContext();
            _dbContext = dbContext;

            try
            {
                _userRefreshRepo = dbContext.GetDbSet<UserRefreshToken>();
            }
            catch
            {
                LibraryLogging.LoggingBroker.LogError($"{nameof(UserRefreshToken)} táblát nem lehet elérni.");
            }
        }

        public async Task<RepositoryResponse> SaveRefreshToken(UserRefreshToken userRefreshToken)
        {

            RepositoryResponse response = new ();

            try
            {
                if (_userRefreshRepo is not null)
                {
                    _userRefreshRepo.Add(userRefreshToken);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                string ErrorString = $"{nameof(UserRefreshTokenRepo<TDbContext>)}\n{nameof(SaveRefreshToken)} metódus. SQL lekérdezés nem hajtható végre!\n{ex.Message}";
                response.ClearAndAddError(ErrorString);

            }
            return response;
        }

        public async Task<RepositoryResponse> DeleteRefreshToken(UserRefreshToken userRefreshTokenToBeDeleted)
        {
            RepositoryResponse response = new ();
            try
            {
                _dbContext.ChangeTracker.Clear();
                _dbContext.Entry(userRefreshTokenToBeDeleted).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string ErrorString = $"{nameof(UserRefreshTokenRepo<TDbContext>)}\n{nameof(DeleteRefreshToken)} metódus. SQL lekérdezés nem hajtható végre!\n{ex.Message}";
                response.ClearAndAddError(ErrorString);
            }
            return response;
        }

        public async Task<UserRefreshToken?> GetRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            if (_userRefreshRepo == null)
            {
                return null;
            }
            else
            {
                UserRefreshToken? userRefreshToken = await _userRefreshRepo.
                    Where(userRefreshToken => userRefreshToken.Token == refreshTokenDto.Token
                    && userRefreshToken.UserId == refreshTokenDto.UserId
                    && userRefreshToken.ExpirationDate > DateTime.Now).FirstOrDefaultAsync();                
                return userRefreshToken;
            }
        }
    }
}
