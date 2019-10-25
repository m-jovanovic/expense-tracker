using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Documents.Documents;
using Application.Queries.Indexes;
using MediatR;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;

namespace Application.Queries.Expenses.GetExpenses
{
    /// <summary>
    /// Represents the handler for the <see cref="GetExpensesForUserQuery"/> query.
    /// </summary>
    public sealed class GetExpensesForUserQueryHandler : IRequestHandler<GetExpensesForUserQuery, IEnumerable<Expense>>
    {
        private readonly IAsyncDocumentSession _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetExpensesForUserQueryHandler"/> class.
        /// </summary>
        /// <param name="session">The async document session instance.</param>
        public GetExpensesForUserQueryHandler(IAsyncDocumentSession session)
        {
            _session = session;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Expense>> Handle(GetExpensesForUserQuery request, CancellationToken cancellationToken)
        {
            return await _session.Query<Expense, Expenses_ByUserId>()
                .Where(e => e.UserId == request.UserId.ToString())
                .ToListAsync(cancellationToken);
        }
    }
}
