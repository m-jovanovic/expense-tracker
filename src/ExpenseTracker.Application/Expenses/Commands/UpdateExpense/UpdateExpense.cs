using System;
using System.Runtime.Serialization;
using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Domain.Primitives;

namespace ExpenseTracker.Application.Expenses.Commands.UpdateExpense
{
    /// <summary>
    /// Represents the command for updating a user expense.
    /// </summary>
    [DataContract]
    public sealed class UpdateExpense : ICommand<Result>
    {
        /// <summary>
        /// Gets the expense identifier.
        /// </summary>
        [DataMember]
        public Guid ExpenseId { get; private set; }

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
