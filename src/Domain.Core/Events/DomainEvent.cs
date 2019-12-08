using System;

namespace Domain.Core.Events
{
    /// <summary>
    /// Represents the base class all domain events inherit from.
    /// </summary>
    public abstract class DomainEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEvent"/> class.
        /// </summary>
        protected internal DomainEvent()
        {
            EventId = Guid.NewGuid();
            OccurredOnUtc = DateTime.UtcNow;
        }

        /// <inheritdoc />
        public Guid EventId { get; }

        /// <inheritdoc />
        public virtual DateTime OccurredOnUtc { get; }
    }
}
