using System;
using Domain.Core.Events;

namespace Domain.Events
{
    /// <summary>
    /// Represents the event that is raised when an amount is withdrawn from the budget.
    /// </summary>
    public sealed class BudgetAmountWithdrawn : BaseDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetAmountWithdrawn"/> class.
        /// </summary>
        /// <param name="budgetId">The budget identifier.</param>
        public BudgetAmountWithdrawn(Guid budgetId)
        {
            BudgetId = budgetId;
        }

        /// <summary>
        /// Gets the budget identifier.
        /// </summary>
        public Guid BudgetId { get; }
    }
}