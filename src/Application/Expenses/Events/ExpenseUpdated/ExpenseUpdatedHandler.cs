using System.Threading;
using System.Threading.Tasks;
using Application.Documents;
using Application.Expenses.Events.ExpenseCreated;
using AutoMapper;
using MediatR;
using Raven.Client.Documents.Session;

namespace Application.Expenses.Events.ExpenseUpdated
{
    /// <summary>
    /// Represents the handler for the <see cref="ExpenseCreated"/> event.
    /// </summary>
    public sealed class ExpenseUpdatedHandler : INotificationHandler<ExpenseUpdated>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncDocumentSession _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseUpdatedHandler"/> class.
        /// </summary>
        /// <param name="mapper">The mapper instance.</param>
        /// <param name="session">The async document session instance.</param>
        public ExpenseUpdatedHandler(IMapper mapper, IAsyncDocumentSession session)
        {
            _mapper = mapper;
            _session = session;
        }

        /// <inheritdoc />
        public async Task Handle(ExpenseUpdated notification, CancellationToken cancellationToken)
        {
            Expense document = await _session.LoadAsync<Expense>(notification.Expense.Id.ToString(), cancellationToken);

            _mapper.Map(notification.Expense, document);
        }
    }
}
