using LibraryCore.Extensions;
using LibraryCore.Model;

namespace Authentication.Shared.Models
{
    public class User : IDbRecord<User>
    {
        public User()
        {
            Id = Guid.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            IsRegisteredUser = true;

            UserRoleId = Guid.Empty;
        }

        public User(Guid id, string firstName, string lastName, string email, bool isRegisteredUser)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            IsRegisteredUser = isRegisteredUser;

            UserRoleId = Guid.Empty;
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsRegisteredUser { get; set; }
        // one - many
        public Guid UserRoleId { get; set; }
        public virtual UserRole UserRole { get; set; }

        public bool IsSystemUser => !IsRegisteredUser;
        public string HungarianFullName => $"{LastName} {FirstName}";
        public bool IsValidUser => !string.IsNullOrEmpty(Email);

        public object Clone()
        {
            return this.CloneJson<User>();
        }

        public bool Equals(User? other)
        {
            if (other is null)
                return false;
            return (FirstName == other.FirstName)
                && (LastName == other.LastName)
                && (Email == other.Email)
                && (IsRegisteredUser=other.IsRegisteredUser);
        }
    }
}
