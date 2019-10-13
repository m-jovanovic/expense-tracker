using System;

namespace Application.Infrastructure
{
    /// <summary>
    /// Represents the response commonly returned from request handlers that create an entity.
    /// </summary>
    public sealed class EntityCreatedResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCreatedResponse"/> class.
        /// </summary>
        /// <param name="id">The entity identifier.</param>
        public EntityCreatedResponse(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("The entity identifier can not be empty.", nameof(id));
            }

            Id = id;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public Guid Id { get; }

        public static implicit operator EntityCreatedResponse(Guid id) => new EntityCreatedResponse(id);
    }
}
