using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ExpenseTracker.Application.Behaviours
{
    /// <summary>
    /// Represents a performance monitoring behavior that wraps a request and monitors execution time.
    /// </summary>
    /// <typeparam name="TRequest">The request type.</typeparam>
    /// <typeparam name="TResponse">The response type.</typeparam>
    public sealed class PerformanceMonitorBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _stopwatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="PerformanceMonitorBehaviour{TRequest,TResponse}"/> class.
        /// </summary>
        public PerformanceMonitorBehaviour()
        {
            _stopwatch = new Stopwatch();
        }

        /// <inheritdoc />
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _stopwatch.Start();

            TResponse response = await next();

            _stopwatch.Stop();

            // TODO: Add Logging later on.
            return response;
        }
    }
}
