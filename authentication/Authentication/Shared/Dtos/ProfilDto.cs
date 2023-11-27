using Authentication.Shared.Models;

namespace Authentication.Shared.Dtos
{
    public class ProfilDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public bool IsValidUser => !string.IsNullOrEmpty(Email);

        public static ProfilDto ConvertToProfilDto(User user)
        {
            return new ProfilDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }
    }
}
