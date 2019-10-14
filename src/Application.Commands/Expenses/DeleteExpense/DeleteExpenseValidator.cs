using FluentValidation;

namespace Application.Commands.Expenses.DeleteExpense
{
    /// <summary>
    /// Represents the validator for the <see cref="DeleteExpense"/> command.
    /// </summary>
    public sealed class DeleteExpenseValidator : AbstractValidator<DeleteExpense>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteExpenseValidator"/> class.
        /// </summary>
        public DeleteExpenseValidator()
        {
            RuleFor(c => c.UserId).NotEmpty();
            RuleFor(c => c.ExpenseId).NotEmpty();
        }
    }
}
