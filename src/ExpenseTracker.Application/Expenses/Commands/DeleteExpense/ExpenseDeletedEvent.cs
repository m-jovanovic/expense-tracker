using System;
using ExpenseTracker.Domain.Events;

namespace ExpenseTracker.Application.Expenses.Commands.DeleteExpense
{
    /// <summary>
    /// Represents the event that fires after an expense is removed.
    /// </summary>
    public sealed class ExpenseDeletedEvent : BaseDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseDeletedEvent"/> class.
        /// </summary>
        /// <param name="expenseId">The expense identifier.</param>
        public ExpenseDeletedEvent(Guid expenseId)
        {
            ExpenseId = expenseId;
        }

        /// <summary>
        /// Gets the expense identifier.
        /// </summary>
        public Guid ExpenseId { get; }
    }
}
