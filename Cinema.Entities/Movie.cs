using System;

namespace Cinema.Entities
{
    public class Movie : BaseEntity
    {
        private string _title = string.Empty;
        private string? _genre;
        private int _duration;
        private string? _ageRating;

        public Movie()
        {
        }

        public Movie(int id, string title, int duration, string? genre = null, string? ageRating = null)
            : base(id)
        {
            Title = title;
            Duration = duration;
            Genre = genre;
            AgeRating = ageRating;
        }

        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Title is required.", nameof(value));
                }

                _title = value.Trim();
            }
        }

        public string? Genre
        {
            get => _genre;
            set => _genre = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        public int Duration
        {
            get => _duration;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Duration must be greater than zero.");
                }

                _duration = value;
            }
        }

        public string? AgeRating
        {
            get => _ageRating;
            set => _ageRating = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }
    }
}
