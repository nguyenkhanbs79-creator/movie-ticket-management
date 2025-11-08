using System;

namespace Cinema.Entities
{
    public abstract class Person : BaseEntity
    {
        private string _fullName = string.Empty;
        private string? _email;
        private string? _phone;

        protected Person()
        {
        }

        protected Person(int id, string fullName, string? email = null, string? phone = null)
            : base(id)
        {
            FullName = fullName;
            Email = email;
            Phone = phone;
        }

        public string FullName
        {
            get => _fullName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Full name is required.", nameof(value));
                }

                _fullName = value.Trim();
            }
        }

        public string? Email
        {
            get => _email;
            set => _email = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        public string? Phone
        {
            get => _phone;
            set => _phone = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        public override string Display()
        {
            return $"{FullName} (#{Id})";
        }
    }
}
