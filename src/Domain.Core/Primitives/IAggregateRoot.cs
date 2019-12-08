using System.Collections.Generic;
using Domain.Core.Events;

namespace Domain.Core.Primitives
{
    /// <summary>
    /// Represents the aggregate root interface.
    /// </summary>
    public interface IAggregateRoot
    {
        /// <summary>
        /// Gets the <see cref="IAggregateRoot"/> instance version.
        /// </summary>
        int Version { get; }

        /// <summary>
        /// Gets the <see cref="IAggregateRoot"/> instance identifier.
        /// </summary>
        string Identifier { get; }

        /// <summary>
        /// Gets the applied domain events within the <see cref="IAggregateRoot"/> instance. This collection is readonly.
        /// </summary>
        IReadOnlyList<IDomainEvent> AppliedDomainEvents { get; }

        /// <summary>
        /// Clears the applied domain events from the <see cref="IAggregateRoot"/> instance.
        /// </summary>
        void ClearAppliedDomainEvents();

        /// <summary>
        /// Applies the specified <see cref="IDomainEvent"/> instance to the <see cref="IAggregateRoot"/> instance.
        /// </summary>
        /// <param name="domainEvent">The domain event to apply.</param>
        void ApplyDomainEvent(IDomainEvent domainEvent);
    }
}
