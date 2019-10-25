using System.Collections.Generic;
using Domain.Core.Events;

namespace Domain.Core.Primitives
{
    /// <summary>
    /// Represents the base class that all aggregate roots derive from.
    /// </summary>
    public abstract class AggregateRoot : Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        /// <summary>
        /// Gets the domain events. This collection is readonly.
        /// </summary>
        public virtual IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        /// <summary>
        /// Clears all the domain events from the <see cref="AggregateRoot"/>.
        /// </summary>
        public virtual void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        /// <summary>
        /// Adds the specified <see cref="IDomainEvent"/> to the <see cref="AggregateRoot"/>.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        protected virtual void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}