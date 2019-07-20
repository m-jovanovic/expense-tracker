using System;

namespace ExpenseTracker.Domain.Primitives
{
    /// <summary>
    /// Represents a marker interface for an auditable entity.
    /// </summary>
    public interface IAuditableEntity
    {
        /// <summary>
        /// Gets the created on date and time in UTC format.
        /// </summary>
        DateTime CreatedOnUtc { get; }

        /// <summary>
        /// Gets the updated on date and time in UTC format.
        /// </summary>
        DateTime? UpdatedOnUtc { get; }
    }
}
