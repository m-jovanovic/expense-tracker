using System;
using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Domain.Aggregates.Expenses;

namespace ExpenseTracker.Application.Expenses.Queries.GetExpense
{
    public sealed class GetExpense : IQuery<Expense?>
    {
        public GetExpense(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
