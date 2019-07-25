using System;

namespace ExpenseTracker.Domain.Exceptions
{
    /// <summary>
    /// Represents an error that occurs in the domain layer.
    /// </summary>
    public class DomainException : Exception
    {
        /// <inheritdoc />
        public DomainException()
        {
        }

        /// <inheritdoc />
        public DomainException(string message)
            : base(message)
        {
        }

        /// <inheritdoc />
        public DomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
