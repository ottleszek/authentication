using Authentication.Server.Datas.Entities;
using AuthenticationLibrary.Shared.Dtos;
using LibaryDatabase.Model;

namespace Authentication.Server.Repos
{
    public interface IUserRefreshTokenRepo
    {
        public Task<RepositoryResponse> SaveRefreshToken(UserRefreshToken userRefreshToken);
        public Task<RepositoryResponse> DeleteRefreshToken(UserRefreshToken userRefreshToken);
        public Task<UserRefreshToken?> GetRefreshToken(RefreshTokenDto refreshTokenDto);
    }
}
