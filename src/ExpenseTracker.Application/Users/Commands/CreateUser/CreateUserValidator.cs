using FluentValidation;

namespace ExpenseTracker.Application.Users.Commands.CreateUser
{
    /// <summary>
    /// Represents the validator for the <see cref="CreateUser"/> command.
    /// </summary>
    public sealed class CreateUserValidator : AbstractValidator<CreateUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserValidator"/> class.
        /// </summary>
        public CreateUserValidator()
        {
            RuleFor(c => c.FirstName).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(c => c.LastName).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(c => c.Email).NotNull().NotEmpty().MaximumLength(255);
        }
    }
}
