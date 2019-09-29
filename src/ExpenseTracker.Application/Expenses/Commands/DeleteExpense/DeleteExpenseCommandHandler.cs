using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ExpenseTracker.Application.Exceptions;
using ExpenseTracker.Domain.Aggregates.Expenses;
using ExpenseTracker.Domain.Aggregates.Users;
using ExpenseTracker.Domain.Primitives;
using MediatR;

namespace ExpenseTracker.Application.Expenses.Commands.DeleteExpense
{
    /// <summary>
    /// Represents the handler for the <see cref="DeleteExpenseCommand"/> command.
    /// </summary>
    public sealed class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand, Result>
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteExpenseCommandHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository instance.</param>
        public DeleteExpenseCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <inheritdoc />
        public async Task<Result> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetUserByIdWithExpensesAsync(request.UserId, request.ExpenseId);

            if (user is null)
            {
                throw new EntityNotFoundException(nameof(User), request.UserId);
            }

            Expense? expense = user.Expenses.SingleOrDefault(e => e.Id == request.ExpenseId);

            if (expense is null)
            {
                throw new EntityNotFoundException(nameof(Expense), request.ExpenseId);
            }

            user.RemoveExpense(expense);

            return Result.Ok();
        }
    }
}