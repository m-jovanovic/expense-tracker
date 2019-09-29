using System;
using System.Linq;
using ExpenseTracker.Domain.Aggregates.Users;

namespace ExpenseTracker.Application.Specification
{
    /// <summary>
    /// Represents the specification for the <see cref="User"/> entity with expenses included.
    /// </summary>
    public sealed class UserWithExpensesSpecification : SpecificationBase<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserWithExpensesSpecification"/> class.
        /// </summary>
        /// <param name="id">The user identifier that will be used for creating the filter criteria.</param>
        /// <param name="expenseId">The expense identifier that will be used for creating the filter criteria.</param>
        public UserWithExpensesSpecification(Guid id, Guid expenseId = default)
            : base(u => u.Id == id && (expenseId == Guid.Empty || u.Expenses.Any(e => e.Id == expenseId)))
        {
            AddInclude(u => u.Expenses);
        }
    }
}