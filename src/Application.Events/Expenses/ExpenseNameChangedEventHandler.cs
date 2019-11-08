using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Documents.Documents;
using Domain.Core.Events;
using Domain.Expenses.Events;
using Raven.Client.Documents.Session;

namespace Application.Events.Expenses
{
    /// <summary>
    /// Represents the handler for the <see cref="ExpenseNameChangedEvent"/> event.
    /// </summary>
    public sealed class ExpenseNameChangedEventHandler : IDomainEventHandler<ExpenseNameChangedEvent>
    {
        private readonly IAsyncDocumentSession _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseNameChangedEventHandler"/> class.
        /// </summary>
        /// <param name="session">The async document session instance.</param>
        public ExpenseNameChangedEventHandler(IAsyncDocumentSession session)
        {
            _session = session;
        }

        /// <inheritdoc />
        public async Task Handle(ExpenseNameChangedEvent notification, CancellationToken cancellationToken)
        {
            if (notification.ExpenseId == Guid.Empty)
            {
                return;
            }

            Expense document = await _session.LoadAsync<Expense>(notification.ExpenseId.ToString(), cancellationToken);

            document.Name = notification.Name;
        }
    }
}
