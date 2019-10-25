using System.Threading;
using System.Threading.Tasks;
using Domain.Aggregates.Expenses;
using Domain.Core.Exceptions;
using Domain.Core.Primitives;
using Domain.Events;
using MediatR;

namespace Application.Commands.Expenses.UpdateExpense
{
    /// <summary>
    /// Represents the handler for the <see cref="CreateExpense"/> command.
    /// </summary>
    public sealed class UpdateExpenseCommandHandler : IRequestHandler<UpdateExpenseCommand, Result>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateExpenseCommandHandler"/> class.
        /// </summary>
        /// <param name="expenseRepository">The expense repository instance.</param>
        /// <param name="mediator">The mediator instance.</param>
        public UpdateExpenseCommandHandler(IExpenseRepository expenseRepository, IMediator mediator)
        {
            _expenseRepository = expenseRepository;
            _mediator = mediator;
        }

        /// <inheritdoc />
        public async Task<Result> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
        {
            Expense? expense = await _expenseRepository.GetByIdAsync(request.ExpenseId);

            if (expense is null)
            {
                throw new EntityNotFoundException(nameof(Expense), request.ExpenseId);
            }

            expense.ChangeAmount(request.Amount);

            await _mediator.Publish(new ExpenseUpdatedEvent(expense), cancellationToken);

            return Result.Ok();
        }
    }
}