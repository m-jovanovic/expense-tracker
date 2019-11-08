using System;
using Domain.Core.Events;

namespace Domain.Expenses.Events
{
    /// <summary>
    /// Represents the event that is raised when the date of an expense is changed.
    /// </summary>
    public sealed class ExpenseDateChangedEvent : BaseDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseDateChangedEvent"/> class.
        /// </summary>
        /// <param name="expenseId">The expense identifier.</param>
        /// <param name="date">The new date of the expense.</param>
        public ExpenseDateChangedEvent(Guid expenseId, DateTime date)
        {
            ExpenseId = expenseId;
            Date = date;
        }

        /// <summary>
        /// Gets the expense identifier.
        /// </summary>
        public Guid ExpenseId { get; }

        /// <summary>
        /// Gets the date identifier.
        /// </summary>
        public DateTime Date { get; }
    }
}
