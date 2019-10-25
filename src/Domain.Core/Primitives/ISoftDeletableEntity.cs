using System;

namespace Domain.Core.Primitives
{
    /// <summary>
    /// Represents an interface for a soft deletable entity.
    /// </summary>
    public interface ISoftDeletableEntity
    {
        /// <summary>
        /// Gets the flag indicating if the entity was deleted.
        /// </summary>
        bool IsDeleted { get; }

        /// <summary>
        /// Gets the deleted on date and time in UTC format.
        /// </summary>
        DateTime? DeletedOnUtc { get; }
    }
}
