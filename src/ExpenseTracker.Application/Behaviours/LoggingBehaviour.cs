using System.Threading;
using System.Threading.Tasks;
using ExpenseTracker.Application.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

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
        private readonly ILogger _logger;
        private readonly IDateTime _dateTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingBehaviour{TRequest,TResponse}"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="dateTime">The date time instance.</param>
        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger, IDateTime dateTime)
        {
            _logger = logger;
            _dateTime = dateTime;
        }

        /// <inheritdoc />
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            string requestName = typeof(TRequest).Name;

            _logger.LogInformation("Handling request: {RequestName} at {Time}", requestName, _dateTime.UtcNow());

            TResponse response = await next();

            _logger.LogInformation("Finished handling request: {RequestName} at {Time}", requestName, _dateTime.UtcNow());

            return response;
        }
    }
}