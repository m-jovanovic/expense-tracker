﻿using FluentValidation;

namespace ExpenseTracker.Application.Expenses.Commands.CreateExpense
{
    /// <summary>
    /// Represents the validator for the <see cref="CreateExpense"/> command.
    /// </summary>
    public sealed class CreateExpenseValidator : AbstractValidator<CreateExpense>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateExpenseValidator"/> class.
        /// </summary>
        public CreateExpenseValidator()
        {
            RuleFor(c => c.UserId).NotEmpty();
            RuleFor(c => c.CurrencyId).NotEmpty();
            RuleFor(c => c.Date).NotEmpty();
        }
    }
}