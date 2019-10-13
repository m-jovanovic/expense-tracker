using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Extensions;
using MediatR;
using Raven.Client.Documents.Session;

namespace Application.Behaviours
{
    /// <summary>
    /// Represents a unit of work behavior that wraps a request and manages the unit of work.
    /// </summary>
    /// <typeparam name="TRequest">The request type.</typeparam>
    /// <typeparam name="TResponse">The response type.</typeparam>
    public sealed class UnitOfWorkBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAsyncDocumentSession _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkBehaviour{TRequest,TResponse}"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work instance.</param>
        /// <param name="session">The async document session instance.</param>
        public UnitOfWorkBehaviour(IUnitOfWork unitOfWork, IAsyncDocumentSession session)
        {
            _unitOfWork = unitOfWork;
            _session = session;
        }

        /// <inheritdoc />
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response = await next();

            if (request.IsQuery())
            {
                return response;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _session.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}
