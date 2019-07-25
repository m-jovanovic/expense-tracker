using System.Collections.Generic;
using ExpenseTracker.Domain.Primitives;

namespace ExpenseTracker.Domain.Aggregates.ExpenseAggregate
{
    /// <summary>
    /// Represents money.
    /// </summary>
    public sealed class Money : ValueObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Money"/> class.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="currency">The currency.</param>
        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        private Money()
        {
        }

        /// <summary>
        /// Gets the amount.
        /// </summary>
        public decimal Amount { get; private set; }

        /// <summary>
        /// Gets the currency.
        /// </summary>
        public Currency Currency { get; private set; }

        /// <summary>
        /// Returns a new <see cref="Money"/> instance with the specified amount and the current currency.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <returns>The new <see cref="Money"/> instance with the specified amount and the current currency.</returns>
        public Money ChangeAmount(decimal amount)
        {
            return new Money(amount, Currency);
        }

        /// <summary>
        /// Returns a new <see cref="Money"/> instance with the specified currency and the current amount.
        /// </summary>
        /// <param name="currency">The currency.</param>
        /// <returns>The new <see cref="Money"/> instance with the specified currency and the current amount.</returns>
        public Money ChangeCurrency(Currency currency)
        {
            return new Money(Amount, currency);
        }

        /// <inheritdoc />
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}
