using System;
using Domain.Core.Events;

namespace Domain.Events
{
    public sealed class ExpenseAmountChangedEvent : BaseDomainEvent
    {
        public ExpenseAmountChangedEvent(Guid expenseId, decimal amountDifference)
        {
            ExpenseId = expenseId;
            AmountDifference = amountDifference;
        }

        public Guid ExpenseId { get; }

        public decimal AmountDifference { get; }
    }
}
