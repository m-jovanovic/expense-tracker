using System;

namespace Domain.Core.Exceptions
{
    /// <summary>
    /// Represents an exception that is raised when an aggregate root is not found.
    /// </summary>
    public sealed class AggregateRootNotFoundException : AggregateRootException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRootNotFoundException"/> class.
        /// </summary>
        /// <param name="id">The aggregate root identifier.</param>
        /// <param name="type">The aggregate root type.</param>
        public AggregateRootNotFoundException(object id, Type type)
            : base(id, type)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRootNotFoundException"/> class.
        /// </summary>
        /// <param name="id">The aggregate root identifier.</param>
        /// <param name="type">The aggregate root type.</param>
        /// <param name="message">The message.</param>
        public AggregateRootNotFoundException(object id, Type type, string message)
            : base(id, type, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRootNotFoundException"/> class.
        /// </summary>
        /// <param name="id">The aggregate root identifier.</param>
        /// <param name="type">The aggregate root type.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public AggregateRootNotFoundException(object id, Type type, string message, Exception innerException)
            : base(id, type, message, innerException)
        {
        }
    }
}
