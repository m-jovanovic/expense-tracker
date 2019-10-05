using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExpenseTracker.Application.Documents;
using MediatR;
using Raven.Client.Documents.Session;

namespace ExpenseTracker.Application.Expenses.Events
{
    /// <summary>
    /// Represents the handler for the <see cref="ExpenseCreated"/> event.
    /// </summary>
    public sealed class ExpenseCreatedHandler : INotificationHandler<ExpenseCreated>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncDocumentSession _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseCreatedHandler"/> class.
        /// </summary>
        /// <param name="mapper">The mapper instance.</param>
        /// <param name="session">The async document session instance.</param>
        public ExpenseCreatedHandler(IMapper mapper, IAsyncDocumentSession session)
        {
            _mapper = mapper;
            _session = session;
        }

        /// <inheritdoc />
        public async Task Handle(ExpenseCreated notification, CancellationToken cancellationToken)
        {
            var document = _mapper.Map<Expense>(notification.Expense);

            await _session.StoreAsync(document, cancellationToken);
        }
    }
}
