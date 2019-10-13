using System;
using System.Collections.Generic;
using Application.Abstractions;
using Application.Documents;

namespace Application.Expenses.Queries.GetExpenses
{
    /// <summary>
    /// Represents the query for getting expenses for a user.
    /// </summary>
    public sealed class GetExpensesForUser : IQuery<IEnumerable<Expense>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetExpensesForUser"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public GetExpensesForUser(Guid userId)
        {
            UserId = userId;
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid UserId { get; }
    }
}
