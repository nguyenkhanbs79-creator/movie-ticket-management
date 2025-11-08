using System;
using System.Security.Cryptography;

namespace Cinema.BLL
{
    /// <summary>
    /// Provides PBKDF2 based password hashing and verification helpers.
    /// </summary>
    public sealed class PasswordHasher
    {
        private const string Algorithm = "PBKDF2-SHA256";
        private const int DefaultIterations = 150_000;
        private const int SaltSize = 24;
        private const int KeySize = 32;

        /// <summary>
        /// Creates a password hash using PBKDF2 with SHA-256.
        /// </summary>
        public string HashPassword(string plainPassword)
        {
            if (plainPassword == null)
            {
                throw new ArgumentNullException(nameof(plainPassword));
            }

            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Pbkdf2(plainPassword, salt, DefaultIterations, KeySize);
            return string.Join(":",
                Algorithm,
                DefaultIterations.ToString(),
                Convert.ToBase64String(salt),
                Convert.ToBase64String(hash));
        }

        /// <summary>
        /// Verifies that the plaintext password matches the stored credential.
        /// </summary>
        /// <param name="plainPassword">The plaintext password to verify.</param>
        /// <param name="storedCredential">The stored credential in the format algorithm:iterations:salt:hash.</param>
        public bool Verify(string plainPassword, string storedCredential)
        {
            if (plainPassword == null)
            {
                throw new ArgumentNullException(nameof(plainPassword));
            }

            if (string.IsNullOrWhiteSpace(storedCredential))
            {
                return false;
            }

            var parts = storedCredential.Split(':');
            if (parts.Length != 4)
            {
                return false;
            }

            if (!parts[0].Equals(Algorithm, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (!int.TryParse(parts[1], out var iterations) || iterations <= 0)
            {
                return false;
            }

            byte[] salt;
            byte[] expectedHash;
            try
            {
                salt = Convert.FromBase64String(parts[2]);
                expectedHash = Convert.FromBase64String(parts[3]);
            }
            catch (FormatException)
            {
                return false;
            }

            var actualHash = Pbkdf2(plainPassword, salt, iterations, expectedHash.Length);
            return CryptographicOperations.FixedTimeEquals(expectedHash, actualHash);
        }

        private static byte[] Pbkdf2(string password, byte[] salt, int iterations, int length)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                return deriveBytes.GetBytes(length);
            }
        }
    }
}
