using FluentValidation;

namespace Application.Commands.Expenses.CreateExpense
{
    /// <summary>
    /// Represents the validator for the <see cref="CreateExpenseCommand"/> command.
    /// </summary>
    public sealed class CreateExpenseCommandValidator : AbstractValidator<CreateExpenseCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateExpenseCommandValidator"/> class.
        /// </summary>
        public CreateExpenseCommandValidator()
        {
            RuleFor(c => c.UserId).NotEmpty();
            RuleFor(c => c.CurrencyId).NotEmpty();
            RuleFor(c => c.Date).NotEmpty();
        }
    }
}
