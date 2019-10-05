using System;
using System.Runtime.Serialization;
using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Domain.Primitives;

namespace ExpenseTracker.Application.Expenses.Commands.DeleteExpense
{
    /// <summary>
    /// Represents the command for deleting a user expense.
    /// </summary>
    [DataContract]
    public sealed class DeleteExpense : ICommand<Result>
    {
        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        [DataMember]
        public Guid UserId { get; private set; }

        /// <summary>
        /// Gets the expense identifier.
        /// </summary>
        [DataMember]
        public Guid ExpenseId { get; private set; }
    }
}
