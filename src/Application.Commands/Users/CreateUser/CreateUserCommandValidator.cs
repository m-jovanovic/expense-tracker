﻿using FluentValidation;

namespace Application.Commands.Users.CreateUser
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
            RuleFor(c => c.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(c => c.LastName).NotEmpty().MaximumLength(100);
            RuleFor(c => c.Email).NotEmpty().MaximumLength(255);
        }
    }
}
