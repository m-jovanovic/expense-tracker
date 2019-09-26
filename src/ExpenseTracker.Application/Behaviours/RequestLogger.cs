using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;

namespace ExpenseTracker.Application.Behaviours
{
    /// <summary>
    /// Represents a request logger that executes before requests and logs them.
    /// </summary>
    /// <typeparam name="TRequest">The request type.</typeparam>
    public sealed class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestLogger{TRequest}"/> class.
        /// </summary>
        public RequestLogger()
        {
            // TODO: Add Logging later on.
        }

        /// <inheritdoc />
        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}