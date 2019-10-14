using System.Threading;
using System.Threading.Tasks;
using Application.Documents.Documents;
using AutoMapper;
using MediatR;
using Raven.Client.Documents.Session;

namespace Application.Commands.Expenses.CreateExpense
{
    /// <summary>
    /// Represents the handler for the <see cref="ExpenseCreated"/> event.
    /// </summary>
    public sealed class ExpenseCreatedHandler : INotificationHandler<Domain.Events.ExpenseCreated>
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
        public async Task Handle(Domain.Events.ExpenseCreated notification, CancellationToken cancellationToken)
        {
            var document = _mapper.Map<Expense>(notification.Expense);

            await _session.StoreAsync(document, cancellationToken);
        }
    }
}
