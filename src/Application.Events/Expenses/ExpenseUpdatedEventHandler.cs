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
    /// Represents the handler for the <see cref="ExpenseUpdatedEvent"/> event.
    /// </summary>
    public sealed class ExpenseUpdatedEventHandler : IDomainEventHandler<ExpenseUpdatedEvent>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncDocumentSession _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseUpdatedEventHandler"/> class.
        /// </summary>
        /// <param name="mapper">The mapper instance.</param>
        /// <param name="session">The async document session instance.</param>
        public ExpenseUpdatedEventHandler(IMapper mapper, IAsyncDocumentSession session)
        {
            _mapper = mapper;
            _session = session;
        }

        /// <inheritdoc />
        public async Task Handle(ExpenseUpdatedEvent notification, CancellationToken cancellationToken)
        {
            Expense document = await _session.LoadAsync<Expense>(notification.Expense.Id.ToString(), cancellationToken);

            _mapper.Map(notification.Expense, document);
        }
    }
}
