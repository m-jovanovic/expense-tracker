using System;
using System.Runtime.Serialization;
using Domain.Core.Primitives;

namespace Application.Commands.Expenses.DeleteExpense
{
    /// <summary>
    /// Represents the command for deleting a user expense.
    /// </summary>
    [DataContract]
    public sealed class DeleteExpenseCommand : ICommand<Result>
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
