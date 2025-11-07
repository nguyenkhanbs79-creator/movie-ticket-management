using System;

namespace Cinema.Entities
{
    public abstract class BaseEntity
    {
        private int _id;

        protected BaseEntity()
        {
        }

        protected BaseEntity(int id)
        {
            Id = id;
        }

        public int Id
        {
            get => _id;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Id cannot be negative.");
                }

                _id = value;
            }
        }

        public virtual string Display()
        {
            return $"#{Id}";
        }
    }
}
