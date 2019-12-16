using System;

namespace Domain.Core.Exceptions
{
    /// <summary>
    /// Represents an error that indicates that the enumeration type is invalid.
    /// </summary>
    public sealed class EnumerationInvalidException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumerationInvalidException"/> class.
        /// </summary>
        /// <param name="type">The enumeration type.</param>
        public EnumerationInvalidException(Type type)
            : base($"The type {type.Name} is not a valid enumeration type.")
        {
            Type = type;
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public Type Type { get; }
    }
}
