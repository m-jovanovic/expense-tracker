using FluentValidation;

namespace Application.Commands.Budgets.CreateBudget
{
    /// <summary>
    /// Represents the validator for the <see cref="CreateBudgetCommand"/> command.
    /// </summary>
    public sealed class CreateBudgetCommandValidator : AbstractValidator<CreateBudgetCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBudgetCommandValidator"/> class.
        /// </summary>
        public CreateBudgetCommandValidator()
        {
            RuleFor(c => c.UserId).NotEmpty();
            RuleFor(c => c.Amount).GreaterThan(0);
            RuleFor(c => c.CurrencyId).GreaterThan(0);
            RuleFor(c => c.StartDate).NotNull();
            RuleFor(c => c.EndDate).NotNull().GreaterThanOrEqualTo(c => c.StartDate);
        }
    }
}
