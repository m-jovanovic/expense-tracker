using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Documents.Documents;
using MediatR;
using Raven.Client.Documents.Session;

namespace Application.Queries.Users.GetUser
{
    /// <summary>
    /// Represents the handler for the <see cref="GetUser"/> query.
    /// </summary>
    public sealed class GetUserHandler : IRequestHandler<GetUser, User?>
    {
        private readonly IAsyncDocumentSession _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserHandler"/> class.
        /// </summary>
        /// <param name="session">The async document session instance.</param>
        public GetUserHandler(IAsyncDocumentSession session)
        {
            _session = session;
        }

        /// <inheritdoc />
        public async Task<User?> Handle(GetUser request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                return default;
            }

            return await _session.LoadAsync<User>(request.Id.ToString(), cancellationToken);
        }
    }
}