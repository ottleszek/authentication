using LibraryCore.Model;

namespace Authentication.Server.Datas.Entities
{
    public class UserRefreshToken : IDbRecord<UserRefreshToken>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
    }
}
