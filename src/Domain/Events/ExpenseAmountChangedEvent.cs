using System;
using Domain.Aggregates.Expenses;
using Domain.Core.Events;

namespace Domain.Events
{
    public sealed class ExpenseAmountChangedEvent : BaseDomainEvent
    {
        public ExpenseAmountChangedEvent(Guid expenseId, Money amountDifference)
        {
            ExpenseId = expenseId;
            AmountDifference = amountDifference;
        }

        public Guid ExpenseId { get; }

        public Money AmountDifference { get; }
    }
}
