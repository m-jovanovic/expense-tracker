using System.Threading;
using System.Threading.Tasks;
using Application.Documents.Documents;
using AutoMapper;
using Domain.Core.Events;
using Domain.Events;
using Raven.Client.Documents.Session;

namespace Application.Events.Users
{
    /// <summary>
    /// Represents the handler for the <see cref="UserCreatedEvent"/> event.
    /// </summary>
    public sealed class UserCreatedEventHandler : IDomainEventHandler<UserCreatedEvent>
    {
        private readonly IAsyncDocumentSession _session;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCreatedEventHandler"/> class.
        /// </summary>
        /// <param name="session">The async document session instance.</param>
        /// <param name="mapper">The mapper instance.</param>
        public UserCreatedEventHandler(IAsyncDocumentSession session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            var document = _mapper.Map<User>(notification.User);

            await _session.StoreAsync(document, cancellationToken);
        }
    }
}
