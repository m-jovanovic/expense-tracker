using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Documents;

namespace ExpenseTracker.Application.Expenses.Queries.GetExpenses
{
    /// <summary>
    /// Represents the query for getting expenses for a user.
    /// </summary>
    [DataContract]
    public sealed class GetExpensesForUser : IQuery<IEnumerable<Expense>>
    {
        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        [DataMember]
        public Guid UserId { get; private set; }
    }
}
