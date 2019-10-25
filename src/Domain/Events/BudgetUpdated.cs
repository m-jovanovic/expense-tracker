using System;
using Domain.Core.Events;

namespace Domain.Events
{
    public sealed class BudgetUpdated : BaseDomainEvent
    {
        public BudgetUpdated(Guid budgetId)
        {
            BudgetId = budgetId;
        }

        public Guid BudgetId { get; }
    }
}
