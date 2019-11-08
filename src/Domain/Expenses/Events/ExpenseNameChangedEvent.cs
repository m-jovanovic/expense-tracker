using System;
using Domain.Core.Events;

namespace Domain.Expenses.Events
{
    /// <summary>
    /// Represents an event that is raised when an expense name is changed.
    /// </summary>
    public sealed class ExpenseNameChangedEvent : BaseDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseNameChangedEvent"/> class.
        /// </summary>
        /// <param name="expenseId">The expense identifier.</param>
        /// <param name="name">The expense name.</param>
        public ExpenseNameChangedEvent(Guid expenseId, string name)
        {
            ExpenseId = expenseId;
            Name = name;
        }

        /// <summary>
        /// Gets the expense identifier.
        /// </summary>
        public Guid ExpenseId { get; }

        /// <summary>
        /// Gets the expense name.
        /// </summary>
        public string Name { get; }
    }
}
