using FluentValidation;

namespace ExpenseTracker.Application.Expenses.Commands.DeleteExpense
{
    /// <summary>
    /// Represents the validator for the <see cref="DeleteExpenseCommand"/> command.
    /// </summary>
    public sealed class DeleteExpenseCommandValidator : AbstractValidator<DeleteExpenseCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteExpenseCommandValidator"/> class.
        /// </summary>
        public DeleteExpenseCommandValidator()
        {
            RuleFor(c => c.UserId).NotEmpty();
            RuleFor(c => c.ExpenseId).NotEmpty();
        }
    }
}
