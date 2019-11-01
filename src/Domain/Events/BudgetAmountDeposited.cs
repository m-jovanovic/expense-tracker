using System;
using Domain.Core.Events;

namespace Domain.Events
{
    public sealed class BudgetAmountDeposited : BaseDomainEvent
    {
        public BudgetAmountDeposited(Guid budgetId)
        {
            BudgetId = budgetId;
        }

        public Guid BudgetId { get; }
    }
}