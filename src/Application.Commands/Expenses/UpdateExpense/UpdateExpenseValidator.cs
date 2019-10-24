﻿using FluentValidation;

namespace Application.Commands.Expenses.UpdateExpense
{
    /// <summary>
    /// Represents the validator for the <see cref="UpdateExpense"/> command.
    /// </summary>
    public sealed class UpdateExpenseValidator : AbstractValidator<UpdateExpense>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateExpenseValidator"/> class.
        /// </summary>
        public UpdateExpenseValidator()
        {
            RuleFor(c => c.ExpenseId).NotEmpty();
            RuleFor(c => c.Date).NotEmpty();
        }
    }
}
