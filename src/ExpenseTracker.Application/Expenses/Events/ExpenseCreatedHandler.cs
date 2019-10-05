using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExpenseTracker.Application.Documents;
using MediatR;
using Raven.Client.Documents.Session;

namespace ExpenseTracker.Application.Expenses.Events
{
    public sealed class ExpenseCreatedHandler : INotificationHandler<ExpenseCreated>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncDocumentSession _session;

        public ExpenseCreatedHandler(IMapper mapper, IAsyncDocumentSession session)
        {
            _mapper = mapper;
            _session = session;
        }

        public async Task Handle(ExpenseCreated notification, CancellationToken cancellationToken)
        {
            var document = _mapper.Map<Expense>(notification.Expense);

            await _session.StoreAsync(document, cancellationToken);
        }
    }
}
