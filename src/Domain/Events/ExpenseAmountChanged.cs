using System;

namespace Domain.Events
{
    public sealed class ExpenseAmountChanged : BaseDomainEvent
    {
        public ExpenseAmountChanged(Guid expenseId, decimal amountDifference)
        {
            ExpenseId = expenseId;
            AmountDifference = amountDifference;
        }

        public Guid ExpenseId { get; }

        public decimal AmountDifference { get; }
    }
}
