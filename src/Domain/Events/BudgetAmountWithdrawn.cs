using System;
using Domain.Core.Events;

namespace Domain.Events
{
    public sealed class BudgetAmountWithdrawn : BaseDomainEvent
    {
        public BudgetAmountWithdrawn(Guid budgetId)
        {
            BudgetId = budgetId;
        }

        public Guid BudgetId { get; }
    }
}