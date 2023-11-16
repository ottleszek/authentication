using System.Security.Cryptography;
using System.Text;

namespace LibraryPassword
{
    public class PasswordManager
    {
        // https://yarkul.com/hash-salt-store-password-in-csharp/

        const int keySize = 64;
        const int iterations = 350000;
        private readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        private readonly char saltDelimeter = ';';
              
        
        public string HashPasword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);

            return string.Join(saltDelimeter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public bool VerifyPassword(string userEnteredPassword, string dbPassword)
        {

            var pwdElements = dbPassword.Split(saltDelimeter);
            var salt = Convert.FromBase64String(pwdElements[0]);
            var hash = Convert.FromBase64String(pwdElements[1]);

            var hashInput = Rfc2898DeriveBytes.Pbkdf2(userEnteredPassword, salt, iterations, hashAlgorithm, keySize);
            
            return CryptographicOperations.FixedTimeEquals(hash, hashInput);
        }

    }
}