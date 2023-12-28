using LibraryCore.Extensions;
using LibraryCore.Model;

namespace Authentication.Server.Datas.Entities
{
    public class UserRefreshToken : IDbRecord<UserRefreshToken>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }

        public object Clone()
        {
            return this.CloneJson<UserRefreshToken>();
        }

        public bool Equals(UserRefreshToken? other)
        {
            if (other is null)
                return false;
            return UserId == other.UserId
                && Token == other.Token
                && ExpirationDate == other.ExpirationDate;
        }
    }
}
