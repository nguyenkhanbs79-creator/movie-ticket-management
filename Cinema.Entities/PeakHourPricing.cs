using System;

namespace Cinema.Entities
{
    public class PeakHourPricing : IPricingStrategy
    {
        private const decimal PeakMultiplier = 1.2m;
        private const int PeakStartHour = 18;
        private const int PeakEndHour = 22;

        public decimal CalculatePrice(decimal basePrice, DateTime showtimeStart)
        {
            if (basePrice <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(basePrice), "Base price must be greater than zero.");
            }

            var isPeakHour = showtimeStart.Hour >= PeakStartHour && showtimeStart.Hour <= PeakEndHour;
            var price = isPeakHour ? basePrice * PeakMultiplier : basePrice;
            return decimal.Round(price, 2, MidpointRounding.AwayFromZero);
        }
    }
}
