using System;
using ExpenseTracker.Domain.Events;

namespace ExpenseTracker.Application.Expenses.Events
{
    /// <summary>
    /// Represents the event that fires after an expense is removed.
    /// </summary>
    public sealed class ExpenseDeleted : BaseDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseDeleted"/> class.
        /// </summary>
        /// <param name="expenseId">The expense identifier.</param>
        public ExpenseDeleted(Guid expenseId)
        {
            ExpenseId = expenseId;
        }

        /// <summary>
        /// Gets the expense identifier.
        /// </summary>
        public Guid ExpenseId { get; }
    }
}
