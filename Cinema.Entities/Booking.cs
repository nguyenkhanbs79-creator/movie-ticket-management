using System;
using System.Collections.Generic;

namespace Cinema.Entities
{
    public class Booking : BaseEntity
    {
        private int _userId;
        private int _showtimeId;
        private DateTime _bookingTime;
        private decimal _totalAmount;

        public Booking()
        {
            Items = new List<BookingItem>();
        }

        public Booking(int id, int userId, int showtimeId, DateTime bookingTime, decimal totalAmount, IEnumerable<BookingItem>? items = null)
            : base(id)
        {
            Items = new List<BookingItem>();
            UserId = userId;
            ShowtimeId = showtimeId;
            BookingTime = bookingTime;
            TotalAmount = totalAmount;

            if (items != null)
            {
                foreach (var item in items)
                {
                    AddItem(item);
                }
            }
        }

        public int UserId
        {
            get => _userId;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "UserId must be greater than zero.");
                }

                _userId = value;
            }
        }

        public int ShowtimeId
        {
            get => _showtimeId;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "ShowtimeId must be greater than zero.");
                }

                _showtimeId = value;
            }
        }

        public DateTime BookingTime
        {
            get => _bookingTime;
            set
            {
                if (value == default)
                {
                    throw new ArgumentException("Booking time must be a valid date.", nameof(value));
                }

                _bookingTime = value;
            }
        }

        public decimal TotalAmount
        {
            get => _totalAmount;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Total amount cannot be negative.");
                }

                _totalAmount = decimal.Round(value, 2, MidpointRounding.AwayFromZero);
            }
        }

        public IList<BookingItem> Items { get; }

        public void AddItem(BookingItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (item.BookingId != 0 && item.BookingId != Id)
            {
                throw new InvalidOperationException("Booking item belongs to a different booking.");
            }

            if (item.BookingId == 0 && Id > 0)
            {
                item.BookingId = Id;
            }

            Items.Add(item);
        }

        public override string Display()
        {
            return $"Booking #{Id} for showtime {ShowtimeId} by user {UserId}";
        }
    }
}
