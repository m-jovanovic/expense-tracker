using System;
using System.Runtime.Serialization;
using Application.Commands.Infrastructure;
using Domain.Core.Primitives;

namespace Application.Commands.Budgets.CreateBudget
{
    /// <summary>
    /// Represents the command for creating a user budget.
    /// </summary>
    [DataContract]
    public sealed class CreateBudgetCommand : ICommand<Result<EntityCreatedResponse>>
    {
        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        [DataMember]
        public Guid UserId { get; }

        /// <summary>
        /// Gets the budget amount.
        /// </summary>
        [DataMember]
        public decimal Amount { get; }

        /// <summary>
        /// Gets the currency identifier.
        /// </summary>
        [DataMember]
        public int CurrencyId { get; }

        /// <summary>
        /// Gets the budget start date.
        /// </summary>
        [DataMember]
        public DateTime StartDate { get; }

        /// <summary>
        /// Gets the budget end date.
        /// </summary>
        [DataMember]
        public DateTime EndDate { get; }
    }
}
