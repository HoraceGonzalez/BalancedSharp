using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp
{
    /// <summary>
    /// Represents a generic api status result.
    /// </summary>
    public class Status<T> : Status
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>The result.</value>
        public T Result { get; set; }
    }

    /// <summary>
    /// Represents a status result.
    /// </summary>
    public class Status
    {
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>The status code.</value>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Returns an OK status with the given result embeded.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Status<T> OK<T>(T result)
        {
            return new Status<T>()
            {
                StatusCode = 200,
                Message = "OK",
                Result = result
            };
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">The message.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static Status<T> Error<T>(string message, int statusCode, T result)
        {
            return new Status<T>()
            {
                StatusCode = statusCode,
                Message = message,
                Result = result
            };
        }
    }
}
