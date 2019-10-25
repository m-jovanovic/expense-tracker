using System.Threading;
using System.Threading.Tasks;
using Application.Documents.Documents;
using AutoMapper;
using Domain.Core.Events;
using Domain.Events;
using Raven.Client.Documents.Session;

namespace Application.Events.Expenses
{
    /// <summary>
    /// Represents the handler for the <see cref="ExpenseCreatedEvent"/> event.
    /// </summary>
    public sealed class ExpenseCreatedEventHandler : IDomainEventHandler<ExpenseCreatedEvent>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncDocumentSession _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseCreatedEventHandler"/> class.
        /// </summary>
        /// <param name="mapper">The mapper instance.</param>
        /// <param name="session">The async document session instance.</param>
        public ExpenseCreatedEventHandler(IMapper mapper, IAsyncDocumentSession session)
        {
            _mapper = mapper;
            _session = session;
        }

        /// <inheritdoc />
        public async Task Handle(ExpenseCreatedEvent notification, CancellationToken cancellationToken)
        {
            var document = _mapper.Map<Expense>(notification.Expense);

            await _session.StoreAsync(document, cancellationToken);
        }
    }
}
