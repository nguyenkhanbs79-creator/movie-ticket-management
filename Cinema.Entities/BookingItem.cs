using System;

namespace Cinema.Entities
{
    public class BookingItem : BaseEntity
    {
        private int _bookingId;
        private string _seatNumber = string.Empty;
        private decimal _unitPrice;

        public BookingItem()
        {
        }

        public BookingItem(int id, int bookingId, string seatNumber, decimal unitPrice)
            : base(id)
        {
            BookingId = bookingId;
            SeatNumber = seatNumber;
            UnitPrice = unitPrice;
        }

        public int BookingId
        {
            get => _bookingId;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "BookingId cannot be negative.");
                }

                _bookingId = value;
            }
        }

        public string SeatNumber
        {
            get => _seatNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Seat number is required.", nameof(value));
                }

                _seatNumber = value.Trim();
            }
        }

        public decimal UnitPrice
        {
            get => _unitPrice;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Unit price must be greater than zero.");
                }

                _unitPrice = decimal.Round(value, 2, MidpointRounding.AwayFromZero);
            }
        }
    }
}
