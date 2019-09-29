using System;
using System.Threading;
using System.Threading.Tasks;
using ExpenseTracker.Application.Exceptions;
using ExpenseTracker.Domain.Aggregates.Expenses;
using ExpenseTracker.Domain.Aggregates.Users;
using ExpenseTracker.Domain.Primitives;
using MediatR;

namespace ExpenseTracker.Application.Expenses.Commands.CreateExpense
{
    /// <summary>
    /// Represents the handler for the <see cref="CreateExpenseCommand"/> command.
    /// </summary>
    public sealed class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IExpenseRepository _expenseRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateExpenseCommandHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository instance.</param>
        /// <param name="expenseRepository">The expense repository instance.</param>
        public CreateExpenseCommandHandler(IUserRepository userRepository, IExpenseRepository expenseRepository)
        {
            _userRepository = userRepository;
            _expenseRepository = expenseRepository;
        }

        /// <inheritdoc />
        public async Task<Result> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetUserByIdAsync(request.UserId);

            if (user is null)
            {
                throw new EntityNotFoundException(nameof(User), request.UserId);
            }

            Maybe<Currency> currencyOrNothing = Enumeration.FromValue<Currency>(request.CurrencyId);

            if (currencyOrNothing.HasNoValue)
            {
                return Result.Fail($"Could not find currency with id {request.CurrencyId}.");
            }

            Currency currency = currencyOrNothing.Value;

            var money = new Money(request.Amount, currency);

            var expense = new Expense(
            	Guid.NewGuid(),
            	user.Id,
            	money,
            	request.Date);

            user.AddExpense(expense);

            _expenseRepository.InsertExpense(expense);

            return Result.Ok();
        }
    }
}