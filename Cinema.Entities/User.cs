using System;

namespace Cinema.Entities
{
    public class User : Person
    {
        /// <summary>
        /// Gets or sets the username used for authentication.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the legacy SHA-256 password hash stored in the database.
        /// This property is kept for backwards compatibility and should not be used
        /// for new credentials.
        /// </summary>
        public string? PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the modern password credential generated via PBKDF2.
        /// The value is stored in the format algorithm:iterations:salt:hash.
        /// </summary>
        public string? PasswordKdf { get; set; }

        public User()
        {
        }

        public User(int id, string fullName, string username, string? passwordHash, string? email = null, string? phone = null)
            : base(id, fullName, email, phone)
        {
            Username = username;
            PasswordHash = passwordHash;
        }

        public override string Display()
        {
            return $"{Username} - {FullName}";
        }
    }
}
