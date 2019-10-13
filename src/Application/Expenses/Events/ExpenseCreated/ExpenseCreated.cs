using Domain.Aggregates.Expenses;
using MediatR;

namespace Application.Expenses.Events.ExpenseCreated
{
    /// <summary>
    /// Represents the event that fires after an expense is created.
    /// </summary>
    public sealed class ExpenseCreated : INotification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseCreated"/> class.
        /// </summary>
        /// <param name="expense">The expense that was created.</param>
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
