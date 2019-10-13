using System.Threading;
using System.Threading.Tasks;
using Application.Documents;
using AutoMapper;
using MediatR;
using Raven.Client.Documents.Session;

namespace Application.Users.Events
{
    /// <summary>
    /// Represents the handler for the <see cref="UserCreated"/> event.
    /// </summary>
    public sealed class UserCreatedHandler : INotificationHandler<UserCreated>
    {
        private readonly IAsyncDocumentSession _session;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCreatedHandler"/> class.
        /// </summary>
        /// <param name="session">The async document session instance.</param>
        /// <param name="mapper">The mapper instance.</param>
        public UserCreatedHandler(IAsyncDocumentSession session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task Handle(UserCreated notification, CancellationToken cancellationToken)
        {
            var document = _mapper.Map<User>(notification.User);

            await _session.StoreAsync(document, cancellationToken);
        }
    }
}
