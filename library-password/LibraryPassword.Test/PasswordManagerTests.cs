using Xunit.Abstractions;

namespace LibraryPassword.Test
{
    public class PasswordManagerTests
    {
        private readonly ITestOutputHelper _output;
        private readonly PasswordManager _passwordManager;

        public PasswordManagerTests(ITestOutputHelper output)
        {
            _output = output;
            _passwordManager = new PasswordManager();
        }

        [Fact]
        public void WhenHasingPassword_ThenReturnsHashAndSalt()
        {
            var hash = _passwordManager.HashPasword("Test@123");

            _output.WriteLine($"Password hash: {hash}");
            Assert.NotNull(hash);
        }

        [Fact]
        public void WhenVerifyingPassword_ThenPositiveVerificationSucceeds()
        {
            var hash = _passwordManager.HashPasword("Test@123");

            var verificationResult = _passwordManager.VerifyPassword("Test@123", hash);

            Assert.True(verificationResult);
        }

        [Fact]
        public void WhenVerifyingPassword_ThenNegativeVerificationSucceeds()
        {
            var hash = _passwordManager.HashPasword("Rossz jelszó");

            var verificationResult = _passwordManager.VerifyPassword("Test@123", hash);

            Assert.False(verificationResult);
        }
    }
}