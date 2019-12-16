using System.Collections.Generic;
using System.Globalization;
using Domain.Core.Events;

namespace Domain.Core.Primitives
{
    /// <summary>
    /// Represents the base class that all aggregate roots derive from.
    /// </summary>
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        private readonly List<IDomainEvent> _appliedDomainEvents = new List<IDomainEvent>();

        /// <inheritdoc />
        public int Version { get; private set; } = 1;

        /// <inheritdoc />
        public virtual string Identifier => $"{GetType().Name.ToLower(CultureInfo.InvariantCulture)}";

        /// <inheritdoc />
        public IReadOnlyList<IDomainEvent> AppliedDomainEvents => _appliedDomainEvents.AsReadOnly();

        /// <inheritdoc />
        public void ClearAppliedDomainEvents()
        {
            _appliedDomainEvents.Clear();
        }

        /// <inheritdoc />
        public void ApplyDomainEvent(IDomainEvent domainEvent)
        {
            ApplyDomainEvent(domainEvent, false);
        }

        /// <summary>
        /// Raises the specified <see cref="IDomainEvent"/> instance within the aggregate root.
        /// </summary>
        /// <param name="domainEvent">The domain event to raise.</param>
        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            ApplyDomainEvent(domainEvent, true);
        }

        /// <summary>
        /// Applies the specified <see cref="IDomainEvent"/> instance and adds it
        /// to the list of applied events if <paramref name="isNewDomainEvent"/> is true.
        /// </summary>
        /// <param name="domainEvent">The domain event to apply.</param>
        /// <param name="isNewDomainEvent">The flag specifying whether or not the domain event is new.</param>
        private void ApplyDomainEvent(IDomainEvent domainEvent, bool isNewDomainEvent)
        {
            ((dynamic)this).ApplyDomainEvent((dynamic)domainEvent);

            Version++;

            if (!isNewDomainEvent)
            {
                return;
            }

            _appliedDomainEvents.Add(domainEvent);
        }
    }
}