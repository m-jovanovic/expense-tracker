using System;

namespace ExpenseTracker.Domain.Exceptions
{
    /// <summary>
    /// Represents an error that indicates that the enumeration type is invalid.
    /// </summary>
    public sealed class EnumerationInvalidException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumerationInvalidException"/> class.
        /// </summary>
        /// <param name="type">The enumeration type.</param>
        public EnumerationInvalidException(Type type)
            : base($"The type {type.Name} is not a valid enumeration type.")
        {
        }
    }
}
