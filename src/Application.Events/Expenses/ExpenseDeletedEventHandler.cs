using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Core.Events;
using Domain.Expenses.Events;
using Raven.Client.Documents.Session;

namespace Application.Events.Expenses
{
    /// <summary>
    /// Represents the handler for the <see cref="ExpenseDeletedEvent"/> event.
    /// </summary>
    public sealed class ExpenseDeletedEventHandler : IDomainEventHandler<ExpenseDeletedEvent>
    {
        private readonly IAsyncDocumentSession _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseDeletedEventHandler"/> class.
        /// </summary>
        /// <param name="session">The async document session instance.</param>
        public ExpenseDeletedEventHandler(IAsyncDocumentSession session)
        {
            _session = session;
        }

        /// <inheritdoc />
        public Task Handle(ExpenseDeletedEvent notification, CancellationToken cancellationToken)
        {
            if (notification.ExpenseId == Guid.Empty)
            {
                return Task.CompletedTask;
            }

            _session.Delete(notification.ExpenseId.ToString());

            return Task.CompletedTask;
        }
    }
}
