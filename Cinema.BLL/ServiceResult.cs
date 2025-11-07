using System;

namespace Cinema.BLL
{
    /// <summary>
    /// Represents a wrapper for returning service responses with status information.
    /// </summary>
    /// <typeparam name="T">Type of the payload returned by the service.</typeparam>
    public class ServiceResult<T>
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public T Data { get; private set; }

        private ServiceResult(bool success, T data, string message)
        {
            Success = success;
            Data = data;
            Message = message ?? string.Empty;
        }

        public static ServiceResult<T> Ok(T data, string message = "OK")
        {
            return new ServiceResult<T>(true, data, message);
        }

        public static ServiceResult<T> Fail(string message)
        {
            return new ServiceResult<T>(false, default(T), string.IsNullOrWhiteSpace(message) ? "Operation failed." : message);
        }
    }
}
