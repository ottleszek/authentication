using LibraryCore.Extensions;
using LibraryCore.Model;

namespace Authentication.Server.Datas.Entities
{
    public class UserIdentification : IDbRecord<UserIdentification>
    {
        public Guid Id { get; set; } //UserId-val egyezik
        public string Password { get; set; } = string.Empty;
        public bool EmailVerified = false;

        public object Clone()
        {
            return this.CloneJson<UserIdentification>();
        }

        public bool Equals(UserIdentification? other)
        {
            if (other is null)
                return false;
            return Password == other.Password
                && EmailVerified == other.EmailVerified;
        }
    }
}
