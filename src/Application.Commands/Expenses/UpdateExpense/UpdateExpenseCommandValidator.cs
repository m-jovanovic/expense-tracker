using FluentValidation;

namespace Application.Commands.Expenses.UpdateExpense
{
    /// <summary>
    /// Represents the validator for the <see cref="UpdateExpenseCommand"/> command.
    /// </summary>
    public sealed class UpdateExpenseCommandValidator : AbstractValidator<UpdateExpenseCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateExpenseCommandValidator"/> class.
        /// </summary>
        public UpdateExpenseCommandValidator()
        {
            RuleFor(c => c.ExpenseId).NotEmpty();
            RuleFor(c => c.Name).NotEmpty().MaximumLength(100);
            RuleFor(c => c.Amount).GreaterThan(decimal.Zero);
            RuleFor(c => c.Date).NotEmpty();
        }
    }
}
