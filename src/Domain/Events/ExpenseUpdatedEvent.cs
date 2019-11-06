using Domain.Aggregates.Expenses;
using Domain.Core.Events;

namespace Domain.Events
{
    /// <summary>
    /// Represents the event that fires after an expense is created.
    /// </summary>
    public sealed class ExpenseUpdatedEvent : BaseDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseUpdatedEvent"/> class.
        /// </summary>
        /// <param name="expense">The expense that was updated.</param>
        public ExpenseUpdatedEvent(Expense expense)
        {
            Expense = expense;
        }

        /// <summary>
        /// Gets the expense.
        /// </summary>
        public Expense Expense { get; }
    }
}
