using System;
using ExpenseTracker.Domain.Primitives;
using MediatR;

namespace ExpenseTracker.Application.Expenses.Commands.DeleteExpense
{
    /// <summary>
    /// Represents the command for deleting a user expense.
    /// </summary>
    public sealed class DeleteExpenseCommand : IRequest<Result>
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the expense identifier.
        /// </summary>
        public Guid ExpenseId { get; set; }
    }
}
