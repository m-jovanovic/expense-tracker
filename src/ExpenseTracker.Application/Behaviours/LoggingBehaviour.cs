using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ExpenseTracker.Application.Behaviours
{
    /// <summary>
    /// Represents a logging behaviour that wraps a request and logs information about it.
    /// </summary>
    /// <typeparam name="TRequest">The request type.</typeparam>
    /// <typeparam name="TResponse">The response type.</typeparam>
    public sealed class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingBehaviour{TRequest,TResponse}"/> class.
        /// </summary>
        public LoggingBehaviour()
        {
        }

        /// <inheritdoc />
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // TODO: Add Logging later on.
            TResponse response = await next();

            return response;
        }
    }
}