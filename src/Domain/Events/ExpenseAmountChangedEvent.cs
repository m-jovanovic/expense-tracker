using System;
using Domain.Aggregates.Expenses;
using Domain.Core.Events;

namespace Domain.Events
{
    /// <summary>
    /// Represents an event that is raised when an expense amount is changed.
    /// </summary>
    public sealed class ExpenseAmountChangedEvent : BaseDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseAmountChangedEvent"/> class.
        /// </summary>
        /// <param name="expenseId">The expense identifier.</param>
        /// <param name="amountDifference">The amount difference.</param>
        public ExpenseAmountChangedEvent(Guid expenseId, Money amountDifference)
        {
            ExpenseId = expenseId;
            AmountDifference = amountDifference;
        }

        /// <summary>
        /// Gets the expense identifier.
        /// </summary>
        public Guid ExpenseId { get; }

        /// <summary>
        /// Gets the amount difference.
        /// </summary>
        public Money AmountDifference { get; }
    }
}
