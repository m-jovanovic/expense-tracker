using System;
using System.Threading;
using System.Threading.Tasks;
using ExpenseTracker.Domain.Aggregates.ExpenseAggregate;
using ExpenseTracker.Domain.Aggregates.UserAggregate;
using ExpenseTracker.Domain.Primitives;
using MediatR;

namespace ExpenseTracker.Application.Expenses.Commands.CreateExpense
{
    public sealed class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, Result>
    {
        private readonly IUserRepository _userRepository;

        public CreateExpenseCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            Maybe<User> userOrNothing = await _userRepository.GetUserWithExpensesByIdAsync(request.UserId);

            if (userOrNothing.HasNoValue)
            {
                return Result.Fail("User could not be found.");
            }

            var currency = Enumeration.FromValue<Currency>(request.CurrencyId);

            var money = new Money(request.Amount, currency);

            var expense = new Expense(Guid.NewGuid(),
                money,
                request.Date);

            User user = userOrNothing.Value;

            user.AddExpense(expense);

            return Result.Ok();
        }
    }
}