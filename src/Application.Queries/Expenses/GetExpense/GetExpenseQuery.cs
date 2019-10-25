using System;
using Application.Documents.Documents;

namespace Application.Queries.Expenses.GetExpense
{
    /// <summary>
    /// Represents the query for getting an expense.
    /// </summary>
    public sealed class GetExpenseQuery : IQuery<Expense?>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetExpenseQuery"/> class.
        /// </summary>
        /// <param name="id">The expense identifier.</param>
        public GetExpenseQuery(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets the expense identifier.
        /// </summary>
        public Guid Id { get; }
    }
}
