using Domain.Aggregates.Expenses;
using MediatR;

namespace Application.Expenses.Events.ExpenseUpdated
{
    /// <summary>
    /// Represents the event that fires after an expense is created.
    /// </summary>
    public sealed class ExpenseUpdated : INotification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseUpdated"/> class.
        /// </summary>
        /// <param name="expense">The expense that was updated.</param>
        public ExpenseUpdated(Expense expense)
        {
            Expense = expense;
        }

        /// <summary>
        /// Gets the expense.
        /// </summary>
        public Expense Expense { get; }
    }
}
