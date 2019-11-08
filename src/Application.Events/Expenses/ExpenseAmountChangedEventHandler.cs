using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Budgets;
using Domain.Core.Events;
using Domain.Core.Exceptions;
using Domain.Expenses;
using Domain.Expenses.Events;

namespace Application.Events.Expenses
{
    /// <summary>
    /// Represents the handler for the <see cref="ExpenseAmountChangedEvent"/> event.
    /// </summary>
    public sealed class ExpenseAmountChangedEventHandler : IDomainEventHandler<ExpenseAmountChangedEvent>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IBudgetRepository _budgetRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseAmountChangedEventHandler"/> class.
        /// </summary>
        /// <param name="expenseRepository">The expense repository instance.</param>
        /// <param name="budgetRepository">The budget repository instance.</param>
        public ExpenseAmountChangedEventHandler(IExpenseRepository expenseRepository, IBudgetRepository budgetRepository)
        {
            _expenseRepository = expenseRepository;
            _budgetRepository = budgetRepository;
        }

        /// <inheritdoc />
        public async Task Handle(ExpenseAmountChangedEvent notification, CancellationToken cancellationToken)
        {
            Expense? expense = await _expenseRepository.GetByIdAsync(notification.ExpenseId);

            if (expense is null)
            {
                throw new EntityNotFoundException(nameof(Expense), notification.ExpenseId);
            }

            IEnumerable<Budget> budgets = await _budgetRepository.GetByDateAndCurrencyAsync(expense.Date, expense.Money.Currency);

            foreach (Budget budget in budgets)
            {
                budget.Deposit(notification.AmountDifference);
            }
        }
    }
}
