using Domain.Core.Exceptions;

namespace Domain.Exceptions
{
    /// <summary>
    /// Represents the exception for a negative amount.
    /// </summary>
    public class NegativeAmountException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NegativeAmountException"/> class.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public NegativeAmountException(decimal amount)
            : base($"The amount {amount:N} can not be negative.")
        {
            Amount = amount;
        }

        /// <summary>
        /// Gets the amount.
        /// </summary>
        public decimal Amount { get; }
    }
}