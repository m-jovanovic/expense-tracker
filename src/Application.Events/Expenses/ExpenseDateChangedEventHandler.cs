using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Application.Documents.Documents;
using AutoMapper;
using Domain.Core.Events;
using Domain.Expenses.Events;
using Raven.Client.Documents.Session;

namespace Application.Events.Expenses
{
    /// <summary>
    /// Represents the handler for the <see cref="ExpenseDateChangedEvent"/> event.
    /// </summary>
    public sealed class ExpenseDateChangedEventHandler : IDomainEventHandler<ExpenseDateChangedEvent>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncDocumentSession _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseDateChangedEventHandler"/> class.
        /// </summary>
        /// <param name="mapper">The mapper instance.</param>
        /// <param name="session">The async document session instance.</param>
        public ExpenseDateChangedEventHandler(IMapper mapper, IAsyncDocumentSession session)
        {
            _mapper = mapper;
            _session = session;
        }

        /// <inheritdoc />
        public async Task Handle(ExpenseDateChangedEvent notification, CancellationToken cancellationToken)
        {
            if (notification.ExpenseId == Guid.Empty)
            {
                return;
            }

            Expense document = await _session.LoadAsync<Expense>(notification.ExpenseId.ToString(), cancellationToken);

            document.Date = notification.Date.ToString(CultureInfo.InvariantCulture);
        }
    }
}
