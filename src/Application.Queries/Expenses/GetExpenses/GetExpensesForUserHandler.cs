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
    /// Represents the handler for the <see cref="GetExpensesForUser"/> query.
    /// </summary>
    public sealed class GetExpensesForUserHandler : IRequestHandler<GetExpensesForUser, IEnumerable<Expense>>
    {
        private readonly IAsyncDocumentSession _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetExpensesForUserHandler"/> class.
        /// </summary>
        /// <param name="session">The async document session instance.</param>
        public GetExpensesForUserHandler(IAsyncDocumentSession session)
        {
            _session = session;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Expense>> Handle(GetExpensesForUser request, CancellationToken cancellationToken)
        {
            return await _session.Query<Expense, Expenses_ByUserId>()
                .Where(e => e.UserId == request.UserId.ToString())
                .ToListAsync(cancellationToken);
        }
    }
}
