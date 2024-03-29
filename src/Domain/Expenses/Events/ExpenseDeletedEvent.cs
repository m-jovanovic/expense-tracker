﻿using System;
using Domain.Core.Events;

namespace Domain.Expenses.Events
{
    /// <summary>
    /// Represents the event that fires after an expense is deleted.
    /// </summary>
    public sealed class ExpenseDeletedEvent : BaseDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseDeletedEvent"/> class.
        /// </summary>
        /// <param name="expenseId">The identifier for the deleted expense.</param>
        public ExpenseDeletedEvent(Guid expenseId)
        {
            ExpenseId = expenseId;
        }

        /// <summary>
        /// Gets the expense identifier.
        /// </summary>
        public Guid ExpenseId { get; }
    }
}
