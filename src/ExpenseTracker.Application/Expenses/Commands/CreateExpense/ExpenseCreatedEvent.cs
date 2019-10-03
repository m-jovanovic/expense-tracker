using ExpenseTracker.Domain.Aggregates.Expenses;
using MediatR;

namespace ExpenseTracker.Application.Expenses.Commands.CreateExpense
{
    /// <summary>
    /// Represents the event that fires after an expense is added.
    /// </summary>
    public sealed class ExpenseCreatedEvent : INotification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseCreatedEvent"/> class.
        /// </summary>
        /// <param name="expense">The expense that was added.</param>
        public ExpenseCreatedEvent(Expense expense)
        {
            Expense = expense;
        }

        /// <summary>
        /// Gets the expense.
        /// </summary>
        public Expense Expense { get; }
    }
}
