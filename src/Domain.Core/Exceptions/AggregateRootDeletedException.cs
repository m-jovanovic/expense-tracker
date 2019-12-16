using System;

namespace Domain.Core.Exceptions
{
    /// <summary>
    /// Represents an exception that is raised when an aggregate root that has been deleted is trying to be read.
    /// </summary>
    public sealed class AggregateRootDeletedException : AggregateRootException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRootDeletedException"/> class.
        /// </summary>
        /// <param name="id">The aggregate root identifier.</param>
        /// <param name="type">The aggregate root type.</param>
        public AggregateRootDeletedException(object id, Type type)
            : base(id, type)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRootDeletedException"/> class.
        /// </summary>
        /// <param name="id">The aggregate root identifier.</param>
        /// <param name="type">The aggregate root type.</param>
        /// <param name="message">The message.</param>
        public AggregateRootDeletedException(object id, Type type, string message)
            : base(id, type, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRootDeletedException"/> class.
        /// </summary>
        /// <param name="id">The aggregate root identifier.</param>
        /// <param name="type">The aggregate root type.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public AggregateRootDeletedException(object id, Type type, string message, Exception innerException)
            : base(id, type, message, innerException)
        {
        }
    }
}
