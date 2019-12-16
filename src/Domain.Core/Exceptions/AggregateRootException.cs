using System;

namespace Domain.Core.Exceptions
{
    /// <summary>
    /// Represents an error related to an aggregate root.
    /// </summary>
    public abstract class AggregateRootException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRootException"/> class.
        /// </summary>
        /// <param name="id">The aggregate root identifier.</param>
        /// <param name="type">The aggregate root type.</param>
        protected AggregateRootException(object id, Type type)
        {
            Id = id;
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRootException"/> class.
        /// </summary>
        /// <param name="id">The aggregate root identifier.</param>
        /// <param name="type">The aggregate root type.</param>
        /// <param name="message">The message.</param>
        protected AggregateRootException(object id, Type type, string message)
            : base(message)
        {
            Id = id;
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRootException"/> class.
        /// </summary>
        /// <param name="id">The aggregate root identifier.</param>
        /// <param name="type">The aggregate root type.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        protected AggregateRootException(object id, Type type, string message, Exception innerException)
            : base(message, innerException)
        {
            Id = id;
            Type = type;
        }

        /// <summary>
        /// Gets the aggregate root identifier.
        /// </summary>
        public object Id { get; }

        /// <summary>
        /// Gets the aggregate root type.
        /// </summary>
        public Type Type { get; }
    }
}
