using System.Threading;
using System.Threading.Tasks;
using Domain.Core.Exceptions;
using Domain.Core.Primitives;
using Domain.Expenses;
using MediatR;

namespace Application.Commands.Expenses.UpdateExpense
{
    /// <summary>
    /// Represents the handler for the <see cref="CreateExpense"/> command.
    /// </summary>
    public sealed class UpdateExpenseCommandHandler : IRequestHandler<UpdateExpenseCommand, Result>
    {
        private readonly IExpenseRepository _expenseRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateExpenseCommandHandler"/> class.
        /// </summary>
        /// <param name="expenseRepository">The expense repository instance.</param>
        public UpdateExpenseCommandHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        /// <inheritdoc />
        public async Task<Result> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
        {
            Expense? expense = await _expenseRepository.GetByIdAsync(request.ExpenseId);

            if (expense is null)
            {
                throw new EntityNotFoundException(nameof(Expense), request.ExpenseId);
            }

            expense.ChangeName(request.Name);

            expense.ChangeAmount(request.Amount);

            expense.ChangeDate(request.Date);

            return Result.Ok();
        }
    }
}