using System;

namespace Cinema.BLL
{
    /// <summary>
    /// Provides guard clause helpers for validating input arguments in services.
    /// </summary>
    public static class Guard
    {
        public static void NotNullOrEmpty(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{fieldName} không được rỗng.", fieldName);
            }
        }

        public static void PositiveInt(int value, string fieldName)
        {
            if (value <= 0)
            {
                throw new ArgumentException($"{fieldName} phải > 0.", fieldName);
            }
        }

        public static void PositiveDecimal(decimal value, string fieldName)
        {
            if (value <= 0)
            {
                throw new ArgumentException($"{fieldName} phải > 0.", fieldName);
            }
        }

        public static void ValidId(int id, string fieldName = "Id")
        {
            if (id <= 0)
            {
                throw new ArgumentException($"{fieldName} phải > 0.", fieldName);
            }
        }

        public static void ValidDateRange(DateTime? from, DateTime? to, string fieldFrom, string fieldTo)
        {
            if (from.HasValue && to.HasValue && from.Value > to.Value)
            {
                throw new ArgumentException($"{fieldFrom} không được lớn hơn {fieldTo}.");
            }
        }

        public static void ValidDateRange(DateTime from, DateTime to, string fieldFrom, string fieldTo)
        {
            if (from > to)
            {
                throw new ArgumentException($"{fieldFrom} không được lớn hơn {fieldTo}.");
            }
        }
    }
}
