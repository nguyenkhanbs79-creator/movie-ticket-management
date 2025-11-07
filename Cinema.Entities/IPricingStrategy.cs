using System;

namespace Cinema.Entities
{
    public interface IPricingStrategy
    {
        decimal CalculatePrice(decimal basePrice, DateTime showtimeStart);
    }
}
