using System;

namespace ExpenseTracker.Application.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs when an entity is not found.
    /// </summary>
    public sealed class EntityNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
        /// </summary>
        /// <param name="name">The name of the entity.</param>
        /// <param name="key">The entity key.</param>
        public EntityNotFoundException(string name, object key)
            : base($"Entity ${name} with key {key} was not found.")
        {
        }
    }
}
