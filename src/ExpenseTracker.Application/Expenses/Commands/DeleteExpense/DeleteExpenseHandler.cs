﻿using System.Threading;
using System.Threading.Tasks;
using ExpenseTracker.Application.Exceptions;
using ExpenseTracker.Application.Expenses.Events;
using ExpenseTracker.Domain.Aggregates.Expenses;
using ExpenseTracker.Domain.Aggregates.Users;
using ExpenseTracker.Domain.Primitives;
using MediatR;

namespace ExpenseTracker.Application.Expenses.Commands.DeleteExpense
{
    /// <summary>
    /// Represents the handler for the <see cref="DeleteExpense"/> command.
    /// </summary>
    public sealed class DeleteExpenseHandler : IRequestHandler<DeleteExpense, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteExpenseHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository instance.</param>
        /// <param name="expenseRepository">The expense repository instance.</param>
        /// <param name="mediator">The mediator instance.</param>
        public DeleteExpenseHandler(IUserRepository userRepository, IExpenseRepository expenseRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _expenseRepository = expenseRepository;
            _mediator = mediator;
        }

        /// <inheritdoc />
        public async Task<Result> Handle(DeleteExpense request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetUserByIdAsync(request.UserId);

            if (user is null)
            {
                throw new EntityNotFoundException(nameof(User), request.UserId);
            }

            Expense? expense = await _expenseRepository.GetExpenseByIdAsync(request.ExpenseId);

            if (expense is null)
            {
                throw new EntityNotFoundException(nameof(Expense), request.ExpenseId);
            }

            if (expense.UserId != user.Id)
            {
                // TODO: Add exception for this?
                return Result.Fail("The expense does not belong to the user.");
            }

            _expenseRepository.DeleteExpense(expense);

            await _mediator.Publish(new ExpenseDeleted(expense.Id), cancellationToken);

            return Result.Ok();
        }
    }
}