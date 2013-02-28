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
        /// Gets or sets the error that occurred.
        /// </summary>
        /// <value>
        /// The error that occurred.
        /// </value>
        public Error Error { get; set; }

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
        public static Status<T> Failed<T>(int statusCode, Error error, T result)
        {
            return new Status<T>()
            {
                StatusCode = statusCode,
                Error = error,
                Result = result
            };
        }
    }
}
