using System;
using Domain.Core.Events;

namespace Domain.Events
{
    /// <summary>
    /// Represents the event that is raised when an amount is deposited to the budget.
    /// </summary>
    public sealed class BudgetAmountDeposited : BaseDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetAmountDeposited"/> class.
        /// </summary>
        /// <param name="budgetId">The budget identifier.</param>
        public BudgetAmountDeposited(Guid budgetId)
        {
            BudgetId = budgetId;
        }

        /// <summary>
        /// Gets the budget identifier.
        /// </summary>
        public Guid BudgetId { get; }
    }
}