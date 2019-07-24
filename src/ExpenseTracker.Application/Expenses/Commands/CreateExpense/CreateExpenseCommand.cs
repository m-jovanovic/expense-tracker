using System;
using ExpenseTracker.Domain.Primitives;
using MediatR;

namespace ExpenseTracker.Application.Expenses.Commands.CreateExpense
{
    /// <summary>
    /// Represents the command for creating a user expense.
    /// </summary>
    public sealed class CreateExpenseCommand : IRequest<Result>
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the expense amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the currency identifier.
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the expense date.
        /// </summary>
        public DateTime Date { get; set; }
    }
}
