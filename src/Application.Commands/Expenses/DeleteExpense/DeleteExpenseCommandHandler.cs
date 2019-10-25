using System.Threading;
using System.Threading.Tasks;
using Domain.Aggregates.Expenses;
using Domain.Aggregates.Users;
using Domain.Core.Exceptions;
using Domain.Core.Primitives;
using Domain.Events;
using MediatR;

namespace Application.Commands.Expenses.DeleteExpense
{
    /// <summary>
    /// Represents the handler for the <see cref="DeleteExpenseCommand"/> command.
    /// </summary>
    public sealed class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteExpenseCommandHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository instance.</param>
        /// <param name="expenseRepository">The expense repository instance.</param>
        /// <param name="mediator">The mediator instance.</param>
        public DeleteExpenseCommandHandler(IUserRepository userRepository, IExpenseRepository expenseRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _expenseRepository = expenseRepository;
            _mediator = mediator;
        }

        /// <inheritdoc />
        public async Task<Result> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetByIdAsync(request.UserId);

            if (user is null)
            {
                throw new EntityNotFoundException(nameof(User), request.UserId);
            }

            Expense? expense = await _expenseRepository.GetByIdAsync(request.ExpenseId);

            if (expense is null)
            {
                throw new EntityNotFoundException(nameof(Expense), request.ExpenseId);
            }

            if (expense.UserId != user.Id)
            {
                // TODO: Add exception for this?
                return Result.Fail("The expense does not belong to the user.");
            }

            _expenseRepository.Delete(expense);

            await _mediator.Publish(new ExpenseDeletedEvent(expense.Id), cancellationToken);

            return Result.Ok();
        }
    }
}