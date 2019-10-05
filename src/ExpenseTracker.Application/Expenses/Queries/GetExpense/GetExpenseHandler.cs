using System;
using System.Threading;
using System.Threading.Tasks;
using ExpenseTracker.Application.Documents;
using ExpenseTracker.Application.Exceptions;
using MediatR;
using Raven.Client.Documents.Session;

namespace ExpenseTracker.Application.Expenses.Queries.GetExpense
{
    public sealed class GetExpenseHandler : IRequestHandler<GetExpense, Expense?>
    {
        private readonly IAsyncDocumentSession _session;

        public GetExpenseHandler(IAsyncDocumentSession session)
        {
            _session = session;
        }

        public async Task<Expense?> Handle(GetExpense request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                throw new EntityNotFoundException(nameof(Expense), request.Id);
            }

            Expense? expense = await _session.LoadAsync<Expense>(request.Id.ToString(), cancellationToken);

            return expense;
        }
    }
}
