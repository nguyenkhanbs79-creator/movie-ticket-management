using System;

namespace Cinema.Entities
{
    public class Showtime : BaseEntity
    {
        private int _movieId;
        private int _auditoriumId;
        private DateTime _startTime;
        private decimal _basePrice;

        public Showtime()
        {
        }

        public Showtime(int id, int movieId, int auditoriumId, DateTime startTime, decimal basePrice)
            : base(id)
        {
            MovieId = movieId;
            AuditoriumId = auditoriumId;
            StartTime = startTime;
            BasePrice = basePrice;
        }

        public int MovieId
        {
            get => _movieId;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "MovieId must be greater than zero.");
                }

                _movieId = value;
            }
        }

        public int AuditoriumId
        {
            get => _auditoriumId;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "AuditoriumId must be greater than zero.");
                }

                _auditoriumId = value;
            }
        }

        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                if (value == default)
                {
                    throw new ArgumentException("Start time must be a valid date.", nameof(value));
                }

                _startTime = value;
            }
        }

        public decimal BasePrice
        {
            get => _basePrice;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Base price must be greater than zero.");
                }

                _basePrice = decimal.Round(value, 2, MidpointRounding.AwayFromZero);
            }
        }

        public override string Display()
        {
            return $"Showtime #{Id} for movie {MovieId} in auditorium {AuditoriumId}";
        }
    }
}
