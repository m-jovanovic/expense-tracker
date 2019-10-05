using System;
using System.Runtime.Serialization;
using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Infrastructure;
using ExpenseTracker.Domain.Primitives;

namespace ExpenseTracker.Application.Expenses.Commands.CreateExpense
{
    /// <summary>
    /// Represents the command for creating a user expense.
    /// </summary>
    [DataContract]
    public sealed class CreateExpense : ICommand<Result<EntityCreatedResponse>>
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
