using System.Linq;
using Application.Documents.Documents;
using Raven.Client.Documents.Indexes;

namespace Application.Queries.Indexes
{
    /// <summary>
    /// Represents the expenses by user identifier index.
    /// </summary>
    public sealed class Expenses_ByUserId : AbstractIndexCreationTask<Expense>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Expenses_ByUserId"/> class.
        /// </summary>
        public Expenses_ByUserId()
        {
            Map = expenses =>
                from e in expenses
                select new
                {
                    e.UserId
                };
        }
    }
}
