using System;

namespace Cinema.Entities
{
    public class Auditorium : BaseEntity
    {
        private string _name = string.Empty;
        private int _seatCount;

        public Auditorium()
        {
        }

        public Auditorium(int id, string name, int seatCount)
            : base(id)
        {
            Name = name;
            SeatCount = seatCount;
        }

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name is required.", nameof(value));
                }

                _name = value.Trim();
            }
        }

        public int SeatCount
        {
            get => _seatCount;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Seat count must be greater than zero.");
                }

                _seatCount = value;
            }
        }
    }
}
