using System;
using System.Collections.Generic;
using Application.Documents.Documents;

namespace Application.Queries.Expenses.GetExpenses
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
