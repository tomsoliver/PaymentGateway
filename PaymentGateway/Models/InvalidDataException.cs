using System;
using System.Collections.Generic;

namespace PaymentGateway.Models
{
    /// <summary>
    /// An exception that is thrown when data is invalid
    /// </summary>
    public class InvalidDataException : Exception
    {
        /// <summary>
        /// An exception that is thrown when data is invalid
        /// </summary>
        public InvalidDataException(IReadOnlyCollection<string> reasons)
        {
            Reasons = reasons;
        }

        /// <summary>
        /// An exception that is thrown when data is invalid
        /// </summary>
        public InvalidDataException(string message, IReadOnlyCollection<string> reasons) : base(message)
        {
            Reasons = reasons;
        }

        public IReadOnlyCollection<string> Reasons { get; }
    }
}
