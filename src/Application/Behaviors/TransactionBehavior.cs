using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Application.Extensions;
using MediatR;

namespace Application.Behaviors
{
    /// <summary>
    /// Represents a transaction behavior that wraps a request and manages the transaction.
    /// </summary>
    /// <typeparam name="TRequest">The request type.</typeparam>
    /// <typeparam name="TResponse">The response type.</typeparam>
    public sealed class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly TransactionOptions _transactionOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionBehavior{TRequest,TResponse}"/> class.
        /// </summary>
        public TransactionBehavior()
        {
            _transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.MaximumTimeout
            };
        }

        /// <inheritdoc />
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request.IsQuery())
            {
                return await next();
            }

            using var transactionScope = new TransactionScope(
                TransactionScopeOption.Required, _transactionOptions, TransactionScopeAsyncFlowOption.Enabled);

            TResponse response = await next();

            transactionScope.Complete();

            return response;
        }
    }
}
