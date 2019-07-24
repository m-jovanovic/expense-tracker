using FluentValidation;

namespace ExpenseTracker.Application.Users.Commands.CreateUser
{
    /// <summary>
    /// Represents the validator for the <see cref="CreateUserCommand"/> command.
    /// </summary>
    public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserCommandValidator"/> class.
        /// </summary>
        public CreateUserCommandValidator()
        {
            RuleFor(c => c.FirstName).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(c => c.LastName).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(c => c.Email).NotNull().NotEmpty().MaximumLength(255);
        }
    }
}
