using System;
using System.Runtime.Serialization;
using Domain.Core.Primitives;

namespace Application.Commands.Expenses.UpdateExpense
{
    /// <summary>
    /// Represents the command for updating a user expense.
    /// </summary>
    [DataContract]
    public sealed class UpdateExpenseCommand : ICommand<Result>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateExpenseCommand"/> class.
        /// </summary>
        public UpdateExpenseCommand()
        {
            Name = string.Empty;
        }

        /// <summary>
        /// Gets the expense identifier.
        /// </summary>
        [DataMember]
        public Guid ExpenseId { get; private set; }

        /// <summary>
        /// Gets the expense name.
        /// </summary>
        [DataMember]
        public string Name { get; private set; }

        /// <summary>
        /// Gets the expense amount.
        /// </summary>
        [DataMember]
        public decimal Amount { get; private set; }

        /// <summary>
        /// Gets the expense date.
        /// </summary>
        [DataMember]
        public DateTime Date { get; private set; }
    }
}
