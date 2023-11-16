using Xunit.Abstractions;

namespace LibraryPassword.Test
{
    public class NoStorePasswordManagerTests
    {
        private readonly ITestOutputHelper _output;
        private readonly NoSaltStorePasswordManager _passwordManager;

        public NoStorePasswordManagerTests(ITestOutputHelper output)
        {
            _output = output;
            _passwordManager = new NoSaltStorePasswordManager();
        }

        [Fact]
        public void WhenHasingPassword_ThenReturnsHashAndSalt()
        {
            var hash = _passwordManager.HashPasword("clear_password", out var salt);

            _output.WriteLine($"Password hash: {hash}");
            _output.WriteLine($"Generated salt: {Convert.ToHexString(salt)}");

            Assert.NotNull(hash);
            Assert.NotNull(salt);
        }

        [Fact]
        public void WhenVerifyingPassword_ThenPositiveVerificationSucceeds()
        {
            var hash = _passwordManager.HashPasword("clear_password", out var salt);

            var verificationResult = _passwordManager.VerifyPassword("clear_password", hash, salt);

            Assert.True(verificationResult);
        }

        [Fact]
        public void WhenVerifyingPassword_ThenNegativeVerificationSucceeds()
        {
            var hash = _passwordManager.HashPasword("clear_password", out var salt);

            var verificationResult = _passwordManager.VerifyPassword("wrong_password", hash, salt);

            Assert.False(verificationResult);
        }
    }
}