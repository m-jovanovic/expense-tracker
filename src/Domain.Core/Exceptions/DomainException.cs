using System;

namespace Domain.Core.Exceptions
{
    /// <summary>
    /// Represents an error that occurs in the domain layer.
    /// </summary>
    public abstract class DomainException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        protected DomainException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        protected DomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
