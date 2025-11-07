using System;

namespace Cinema.Entities
{
    public class User : Person
    {
        private string _username = string.Empty;
        private string _passwordHash = string.Empty;

        public User()
        {
        }

        public User(int id, string fullName, string username, string passwordHash, string? email = null, string? phone = null)
            : base(id, fullName, email, phone)
        {
            Username = username;
            PasswordHash = passwordHash;
        }

        public string Username
        {
            get => _username;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Username is required.", nameof(value));

                _username = value.Trim();
            }
        }

        public string PasswordHash
        {
            get => _passwordHash;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Password hash is required.", nameof(value));

                _passwordHash = value;
            }
        }

        public override string Display()
        {
            return $"{Username} - {FullName}";
        }
    }
}
