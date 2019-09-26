using System.Threading;
using System.Threading.Tasks;
using ExpenseTracker.Application.Extensions;
using ExpenseTracker.Domain.Abstractions;
using MediatR;

namespace ExpenseTracker.Application.Behaviours
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

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkBehaviour{TRequest,TResponse}"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work instance.</param>
        public UnitOfWorkBehaviour(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response = await next();

            // Only use the unit of work if the current request is a command.
            // There is no need to save changes in any other case.
            if (request.IsCommand())
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return response;
        }
    }
}
