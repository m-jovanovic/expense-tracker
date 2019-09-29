using System;
using System.Linq;
using ExpenseTracker.Domain.Aggregates.Users;

namespace ExpenseTracker.Application.QuerySpecification
{
    /// <summary>
    /// Represents the specification for the <see cref="User"/> entity with expenses included.
    /// </summary>
    public sealed class UserWithExpensesQuerySpecification : QuerySpecificationBase<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserWithExpensesQuerySpecification"/> class.
        /// </summary>
        /// <param name="id">The user identifier that will be used for creating the filter criteria.</param>
        /// <param name="expenseId">The expense identifier that will be used for creating the filter criteria.</param>
        public UserWithExpensesQuerySpecification(Guid id, Guid expenseId = default)
            : base(u => u.Id == id && (expenseId == Guid.Empty || u.Expenses.Any(e => e.Id == expenseId)))
        {
            AddInclude(u => u.Expenses);
        }
    }
}