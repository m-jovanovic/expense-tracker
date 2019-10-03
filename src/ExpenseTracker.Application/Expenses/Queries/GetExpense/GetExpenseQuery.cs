using System;
using System.Runtime.Serialization;
using ExpenseTracker.Domain.Aggregates.Expenses;
using MediatR;

namespace ExpenseTracker.Application.Expenses.Queries.GetExpense
{
    [DataContract]
    public sealed class GetExpenseQuery : IRequest<Expense?>
    {
        public GetExpenseQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
