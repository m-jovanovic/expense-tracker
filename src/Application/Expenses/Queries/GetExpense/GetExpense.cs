using System;
using Application.Abstractions;
using Application.Documents;

namespace Application.Expenses.Queries.GetExpense
{
    /// <summary>
    /// Represents the query for getting an expense.
    /// </summary>
    public sealed class GetExpense : IQuery<Expense?>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetExpense"/> class.
        /// </summary>
        /// <param name="id">The expense identifier.</param>
        public GetExpense(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets the expense identifier.
        /// </summary>
        public Guid Id { get; }
    }
}
