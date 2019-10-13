using System;
using MediatR;

namespace Application.Expenses.Events.ExpenseDeleted
{
    /// <summary>
    /// Represents the event that fires after an expense is deleted.
    /// </summary>
    public sealed class ExpenseDeleted : INotification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseDeleted"/> class.
        /// </summary>
        /// <param name="expenseId">The identifier for the deleted expense.</param>
        public ExpenseDeleted(Guid expenseId)
        {
            ExpenseId = expenseId;
        }

        /// <summary>
        /// Gets the expense identifier.
        /// </summary>
        public Guid ExpenseId { get; }
    }
}
