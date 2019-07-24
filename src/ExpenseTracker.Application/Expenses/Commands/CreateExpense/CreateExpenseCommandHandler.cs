﻿using System;
using System.Threading;
using System.Threading.Tasks;
using ExpenseTracker.Application.Exceptions;
using ExpenseTracker.Domain.Aggregates.ExpenseAggregate;
using ExpenseTracker.Domain.Aggregates.UserAggregate;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateExpenseCommand"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository instance.</param>
        public CreateExpenseCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <inheritdoc />
        public async Task<Result> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            Maybe<User> userOrNothing = await _userRepository.GetUserWithExpensesByIdAsync(request.UserId);

            if (userOrNothing.HasNoValue)
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

            User user = userOrNothing.Value;

            var expense = new Expense(Guid.NewGuid(),
                user.Id,
                money,
                request.Date);

            user.AddExpense(expense);

            return Result.Ok();
        }
    }
}