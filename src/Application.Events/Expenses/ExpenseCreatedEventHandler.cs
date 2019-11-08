using System.Threading;
using System.Threading.Tasks;
using Application.Documents.Documents;
using Domain.Core.Events;
using Domain.Expenses.Events;
using Raven.Client.Documents.Session;

namespace Application.Events.Expenses
{
    /// <summary>
    /// Represents the handler for the <see cref="ExpenseCreatedEvent"/> event.
    /// </summary>
    public sealed class ExpenseCreatedEventHandler : IDomainEventHandler<ExpenseCreatedEvent>
    {
        private readonly IAsyncDocumentSession _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseCreatedEventHandler"/> class.
        /// </summary>
        /// <param name="session">The async document session instance.</param>
        public ExpenseCreatedEventHandler(IAsyncDocumentSession session)
        {
            _session = session;
        }

        /// <inheritdoc />
        public async Task Handle(ExpenseCreatedEvent notification, CancellationToken cancellationToken)
        {
            var document = new Expense
            {
                Id = notification.ExpenseId.ToString(),
                UserId = notification.UserId.ToString(),
                Name = notification.Name,
                Amount = notification.Money.Amount,
                CurrencySymbol = notification.Money.Currency.Symbol
            };

            await _session.StoreAsync(document, cancellationToken);
        }
    }
}
