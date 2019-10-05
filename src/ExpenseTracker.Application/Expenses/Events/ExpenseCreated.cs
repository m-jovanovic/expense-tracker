using ExpenseTracker.Domain.Aggregates.Expenses;
using MediatR;

namespace ExpenseTracker.Application.Expenses.Events
{
    /// <summary>
    /// Represents the event that fires after an expense is added.
    /// </summary>
    public sealed class ExpenseCreated : INotification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseCreated"/> class.
        /// </summary>
        /// <param name="expense">The expense that was added.</param>
        public ExpenseCreated(Expense expense)
        {
            Expense = expense;
        }

        /// <summary>
        /// Gets the expense.
        /// </summary>
        public Expense Expense { get; }
    }
}
