using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Infrastructure;
using Domain.Core.Exceptions;
using Domain.Core.Primitives;
using Domain.Expenses;
using Domain.Expenses.Events;
using Domain.Users;
using MediatR;

namespace Application.Commands.Expenses.CreateExpense
{
    /// <summary>
    /// Represents the handler for the <see cref="CreateExpenseCommand"/> command.
    /// </summary>
    public sealed class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, Result<EntityCreatedResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateExpenseCommandHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository instance.</param>
        /// <param name="expenseRepository">The expense repository instance.</param>
        /// <param name="mediator">The mediator instance.</param>
        public CreateExpenseCommandHandler(IUserRepository userRepository, IExpenseRepository expenseRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _expenseRepository = expenseRepository;
            _mediator = mediator;
        }

        /// <inheritdoc />
        public async Task<Result<EntityCreatedResponse>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
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

            var expense = new Expense(Guid.NewGuid(), user.Id, request.Name, money, request.Date);

            _expenseRepository.Insert(expense);

            await _mediator.Publish(
                new ExpenseCreatedEvent(expense.Id, expense.UserId, expense.Name, expense.Money, expense.Date), cancellationToken);

            return Result.Ok<EntityCreatedResponse>(expense.Id);
        }
    }
}