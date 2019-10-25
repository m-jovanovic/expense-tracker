using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors
{
    /// <summary>
    /// Represents a performance monitoring behavior that wraps a request and monitors execution time.
    /// </summary>
    /// <typeparam name="TRequest">The request type.</typeparam>
    /// <typeparam name="TResponse">The response type.</typeparam>
    public sealed class PerformanceMonitorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _stopwatch;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PerformanceMonitorBehavior{TRequest,TResponse}"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        public PerformanceMonitorBehavior(ILogger<PerformanceMonitorBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
            _stopwatch = new Stopwatch();
        }

        /// <inheritdoc />
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _stopwatch.Start();

            TResponse response = await next();

            _stopwatch.Stop();

            string requestName = typeof(TRequest).Name;

            if (_stopwatch.ElapsedMilliseconds > 500)
            {
                // TODO: Send some kind of notification?
                _logger.LogWarning("Request {Name} completed in {}ms", requestName, _stopwatch.ElapsedMilliseconds);
            }
            else
            {
                _logger.LogInformation("Request {Name} completed in {}ms", requestName, _stopwatch.ElapsedMilliseconds);
            }

            return response;
        }
    }
}
