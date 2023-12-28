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
            UserRoleId = Guid.Empty;
        }

        public User(Guid id, string firstName, string lastName, string email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserRoleId = Guid.Empty;
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid UserRoleId { get; set; }

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
                && (Email == other.Email);
        }
    }
}
