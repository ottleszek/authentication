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

        public object Clone()
        {
            return this.CloneJson<UserRole>();
        }
    }
}
