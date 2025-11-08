namespace Cinema.BLL
{
    /// <summary>
    /// Standardized error codes returned in <see cref="ServiceResult{T}"/>.
    /// </summary>
    public enum ErrorCode
    {
        None = 0,
        InvalidInput = 1,
        AuthFailed = 2,
        DbError = 3,
        SeatTaken = 4,
        NotFound = 5
    }
}
