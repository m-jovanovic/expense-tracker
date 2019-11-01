﻿using System;
using System.Runtime.Serialization;
using Application.Commands.Infrastructure;
using Domain.Core.Primitives;

namespace Application.Commands.Expenses.CreateExpense
{
    /// <summary>
    /// Represents the command for creating a user expense.
    /// </summary>
    [DataContract]
    public sealed class CreateExpenseCommand : ICommand<Result<EntityCreatedResponse>>
    {
        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        [DataMember]
        public Guid UserId { get; private set; }

        /// <summary>
        /// Gets the expense amount.
        /// </summary>
        [DataMember]
        public decimal Amount { get; private set; }

        /// <summary>
        /// Gets the currency identifier.
        /// </summary>
        [DataMember]
        public int CurrencyId { get; private set; }

        /// <summary>
        /// Gets the expense date.
        /// </summary>
        [DataMember]
        public DateTime Date { get; private set; }
    }
}