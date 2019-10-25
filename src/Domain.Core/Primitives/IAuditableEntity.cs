using System;

namespace Domain.Core.Primitives
{
    /// <summary>
    /// Represents an interface for an auditable entity.
    /// </summary>
    public interface IAuditableEntity
    {
        /// <summary>
        /// Gets the created on date and time in UTC format.
        /// </summary>
        DateTime CreatedOnUtc { get; }

        /// <summary>
        /// Gets the modified on date and time in UTC format.
        /// </summary>
        DateTime? ModifiedOnUtc { get; }
    }
}
