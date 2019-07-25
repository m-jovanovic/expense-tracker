using System;

namespace ExpenseTracker.Domain.Events
{
    /// <summary>
    /// Represents the event that fires after an expense is removed.
    /// </summary>
    public sealed class ExpenseRemovedEvent : BaseDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseRemovedEvent"/> class.
        /// </summary>
        /// <param name="expenseId">The expense identifier.</param>
        public ExpenseRemovedEvent(Guid expenseId)
        {
            ExpenseId = expenseId;
        }

        /// <summary>
        /// Gets the expense identifier.
        /// </summary>
        public Guid ExpenseId { get; }
    }
}
