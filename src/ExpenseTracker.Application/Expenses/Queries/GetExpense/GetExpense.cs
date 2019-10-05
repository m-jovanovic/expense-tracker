using System;
using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Documents;

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
