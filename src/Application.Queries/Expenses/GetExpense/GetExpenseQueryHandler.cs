using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Documents.Documents;
using MediatR;
using Raven.Client.Documents.Session;

namespace Application.Queries.Expenses.GetExpense
{
    /// <summary>
    /// Represents the handler for the <see cref="GetExpenseQuery"/> query.
    /// </summary>
    public sealed class GetExpenseQueryHandler : IRequestHandler<GetExpenseQuery, Expense?>
    {
        private readonly IAsyncDocumentSession _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetExpenseQueryHandler"/> class.
        /// </summary>
        /// <param name="session">The async document session instance.</param>
        public GetExpenseQueryHandler(IAsyncDocumentSession session)
        {
            _session = session;
        }

        /// <inheritdoc />
        public async Task<Expense?> Handle(GetExpenseQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                return default;
            }

            return await _session.LoadAsync<Expense>(request.Id.ToString(), cancellationToken);
        }
    }
}
