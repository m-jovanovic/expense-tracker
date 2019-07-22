using System.Threading;
using System.Threading.Tasks;
using ExpenseTracker.Application.Extensions;
using ExpenseTracker.Domain.Abstractions;
using MediatR;

namespace ExpenseTracker.Application.Behaviors
{
    public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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
