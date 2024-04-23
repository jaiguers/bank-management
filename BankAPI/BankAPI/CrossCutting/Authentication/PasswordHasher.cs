using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace BankAPI.CrossCutting.Authentication
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SALT_SIZE = 16; // 128/8
        private const int SALT_LENGTH = 32; // 256/8
        private const int ITERATIONS = 10000;
        private static readonly HashAlgorithmName hashAlgorithmName = HashAlgorithmName.SHA256;
        private const char DELIMITER = ';';

        public string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SALT_SIZE);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, ITERATIONS, hashAlgorithmName, SALT_LENGTH);

            return string.Join(DELIMITER, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public bool Verify(string passwordHash, string password)
        {
            var items = passwordHash.Split(DELIMITER);
            var salt = Convert.FromBase64String(items[0]);
            var hash = Convert.FromBase64String(items[1]);

            var hashEntered = Rfc2898DeriveBytes.Pbkdf2(password, salt, ITERATIONS, hashAlgorithmName, SALT_LENGTH);
            return CryptographicOperations.FixedTimeEquals(hash, hashEntered);
        }
    }
}
