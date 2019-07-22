using System;
using ExpenseTracker.Domain.Primitives;
using MediatR;

namespace ExpenseTracker.Application.Expenses.Commands.CreateExpense
{
    public sealed class CreateExpenseCommand : IRequest<Result>
    {
        public Guid UserId { get; set; }

        public decimal Amount { get; set; }

        public int CurrencyId { get; set; }

        public DateTime Date { get; set; }
    }
}
