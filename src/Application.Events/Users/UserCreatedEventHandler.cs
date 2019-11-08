using System.Threading;
using System.Threading.Tasks;
using Application.Documents.Documents;
using Domain.Core.Events;
using Domain.Users.Events;
using Raven.Client.Documents.Session;

namespace Application.Events.Users
{
    /// <summary>
    /// Represents the handler for the <see cref="UserCreatedEvent"/> event.
    /// </summary>
    public sealed class UserCreatedEventHandler : IDomainEventHandler<UserCreatedEvent>
    {
        private readonly IAsyncDocumentSession _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCreatedEventHandler"/> class.
        /// </summary>
        /// <param name="session">The async document session instance.</param>
        public UserCreatedEventHandler(IAsyncDocumentSession session)
        {
            _session = session;
        }

        /// <inheritdoc />
        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            var document = new User
            {
                Id = notification.UserId.ToString(),
                FirstName = notification.FirstName,
                LastName = notification.LastName,
                Email = notification.Email
            };

            await _session.StoreAsync(document, cancellationToken);
        }
    }
}
