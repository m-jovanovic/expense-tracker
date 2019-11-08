using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Infrastructure;
using Domain.Budgets;
using Domain.Core.Exceptions;
using Domain.Core.Primitives;
using Domain.Expenses;
using Domain.Users;
using MediatR;

namespace Application.Commands.Budgets.CreateBudget
{
    /// <summary>
    /// Represents the handler for the <see cref="CreateBudgetCommand"/> command.
    /// </summary>
    public sealed class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, Result<EntityCreatedResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IBudgetRepository _budgetRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBudgetCommandHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository instance.</param>
        /// <param name="budgetRepository">The budget repository instance.</param>
        public CreateBudgetCommandHandler(IUserRepository userRepository, IBudgetRepository budgetRepository)
        {
            _userRepository = userRepository;
            _budgetRepository = budgetRepository;
        }

        /// <inheritdoc />
        public async Task<Result<EntityCreatedResponse>> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetByIdAsync(request.UserId);

            if (user is null)
            {
                throw new EntityNotFoundException(nameof(User), request.UserId);
            }

            var currency = Enumeration.FromValue<Currency>(request.CurrencyId);

            if (currency is null)
            {
                return Result.Fail<EntityCreatedResponse>($"Could not find currency with id {request.CurrencyId}.");
            }

            var money = new Money(request.Amount, currency);

            var budget = new Budget(Guid.NewGuid(), user.Id, money, request.StartDate, request.EndDate);

            _budgetRepository.Insert(budget);

            // TODO: Publish budget created event.
            return Result.Ok(new EntityCreatedResponse(budget.Id));
        }
    }
}
