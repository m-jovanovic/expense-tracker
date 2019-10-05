using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Raven.Client.Documents.Session;

namespace ExpenseTracker.Application.Expenses.Events
{
    public sealed class ExpenseDeletedHandler : INotificationHandler<ExpenseDeleted>
    {
        private readonly IAsyncDocumentSession _session;

        public ExpenseDeletedHandler(IAsyncDocumentSession session)
        {
            _session = session;
        }

        public Task Handle(ExpenseDeleted notification, CancellationToken cancellationToken)
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
