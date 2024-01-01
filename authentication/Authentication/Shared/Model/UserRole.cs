using LibraryCore.Extensions;
using LibraryCore.Model;

namespace Authentication.Shared.Models
{
    public class UserRole : IDbRecord<UserRole>
    {
        public UserRole()
        {
            Id = Guid.Empty;
            EnglishName = string.Empty;
            Name = string.Empty;
        }

        public UserRole(Guid id, string englishName, string name)
        {
            Id = id;
            EnglishName = englishName;
            Name = name;
        }

        public Guid Id{ get; set; }
        public string EnglishName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<User> Users { get; set; }

        public object Clone()
        {
            //   return this.CloneJson<UserRole>();
            return new UserRole
            {
                Id = Id,
                EnglishName = EnglishName,
                Name = Name
            };
        }

        public bool Equals(UserRole? other)
        {
            if (other is null)
                return false;
            return (EnglishName == other.EnglishName)
                && (Name == other.Name);
        }
    }
}
