using System;

namespace Cinema.Entities
{
    public class FlatPricing : IPricingStrategy
    {
        public decimal CalculatePrice(decimal basePrice, DateTime showtimeStart)
        {
            if (basePrice <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(basePrice), "Base price must be greater than zero.");
            }

            return decimal.Round(basePrice, 2, MidpointRounding.AwayFromZero);
        }
    }
}
