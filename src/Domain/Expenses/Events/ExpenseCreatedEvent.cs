﻿using System;
using Domain.Core.Events;

namespace Domain.Expenses.Events
{
    /// <summary>
    /// Represents the event that fires after an expense is created.
    /// </summary>
    public sealed class ExpenseCreatedEvent : BaseDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseCreatedEvent"/> class.
        /// </summary>
        /// <param name="expenseId">The expense identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="name">The expense name.</param>
        /// <param name="money">The expense money.</param>
        /// <param name="date">The expense date.</param>
        public ExpenseCreatedEvent(Guid expenseId, Guid userId, string name, Money money, DateTime date)
        {
            ExpenseId = expenseId;
            UserId = userId;
            Name = name;
            Money = money;
            Date = date;
        }

        /// <summary>
        /// Gets the expense identifier.
        /// </summary>
        public Guid ExpenseId { get; }

        /// <summary>
        /// Gets the expense' user identifier.
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Gets the expense name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the expense money.
        /// </summary>
        public Money Money { get; }

        /// <summary>
        /// Gets the expense date.
        /// </summary>
        public DateTime Date { get; }
    }
}
