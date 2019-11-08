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
    public sealed class ExpenseAmountChangedEventHandler : IDomainEventHandler<ExpenseAmountChangedEvent>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IBudgetRepository _budgetRepository;

        public ExpenseAmountChangedEventHandler(IExpenseRepository expenseRepository, IBudgetRepository budgetRepository)
        {
            _expenseRepository = expenseRepository;
            _budgetRepository = budgetRepository;
        }

        public async Task Handle(ExpenseAmountChangedEvent notification, CancellationToken cancellationToken)
        {
            Expense? expense = await _expenseRepository.GetByIdAsync(notification.ExpenseId);

            if (expense is null)
            {
                throw new DomainException($"The expense with id {notification.ExpenseId} was not found.");
            }

            IEnumerable<Budget> budgets = await _budgetRepository.GetByDateAndCurrencyAsync(expense.Date, expense.Money.Currency);

            foreach (Budget budget in budgets)
            {
                budget.Deposit(notification.AmountDifference);
            }
        }
    }
}
