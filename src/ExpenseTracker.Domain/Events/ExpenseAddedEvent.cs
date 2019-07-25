using System;

namespace ExpenseTracker.Domain.Events
{
    /// <summary>
    /// Represents the event that fires after an expense is added.
    /// </summary>
    public sealed class ExpenseAddedEvent : BaseDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseAddedEvent"/> class.
        /// </summary>
        /// <param name="expenseId">The expense identifier.</param>
        public ExpenseAddedEvent(Guid expenseId)
        {
            ExpenseId = expenseId;
        }

        /// <summary>
        /// Gets the expense identifier.
        /// </summary>
        public Guid ExpenseId { get; }
    }
}
