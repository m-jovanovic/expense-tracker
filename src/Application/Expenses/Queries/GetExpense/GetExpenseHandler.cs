using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Documents;
using MediatR;
using Raven.Client.Documents.Session;

namespace Application.Expenses.Queries.GetExpense
{
    /// <summary>
    /// Represents the handler for the <see cref="GetExpense"/> query.
    /// </summary>
    public sealed class GetExpenseHandler : IRequestHandler<GetExpense, Expense?>
    {
        private readonly IAsyncDocumentSession _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetExpenseHandler"/> class.
        /// </summary>
        /// <param name="session">The async document session instance.</param>
        public GetExpenseHandler(IAsyncDocumentSession session)
        {
            _session = session;
        }

        /// <inheritdoc />
        public async Task<Expense?> Handle(GetExpense request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                return default;
            }

            return await _session.LoadAsync<Expense>(request.Id.ToString(), cancellationToken);
        }
    }
}
