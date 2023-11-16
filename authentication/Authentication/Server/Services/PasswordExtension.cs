using LibraryPassword;

namespace Authentication.Server.Services
{
    public class PasswordExtension
    {
        public static string HashPassword(string plainPassword)
        {
            PasswordManager passwordManager = new();
            string password = passwordManager.HashPasword(plainPassword);
            return password;
        }
    }
}
