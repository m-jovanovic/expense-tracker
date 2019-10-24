using System;
using System.Collections.Generic;
using Domain.Exceptions;
using Domain.Primitives;

namespace Domain.Aggregates.Expenses
{
    /// <summary>
    /// Represents money.
    /// </summary>
    public sealed class Money : ValueObject
    {
        /// <summary>
        /// Returns an empty <see cref="Money"/> object.
        /// </summary>
        internal static readonly Money Empty = new Money();

        /// <summary>
        /// Initializes a new instance of the <see cref="Money"/> class.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="currency">The currency.</param>
        /// <exception cref="NegativeAmountException"> if the specified amount is negative.</exception>
        /// <exception cref="ArgumentException"> if the specified currency does not exist.</exception>
        public Money(decimal amount, Currency currency)
        {
            if (amount < decimal.Zero)
            {
                throw new NegativeAmountException(amount);
            }

            if (currency.Equals(Currency.None))
            {
                throw new ArgumentException("The currency is required.", nameof(currency));
            }

            Amount = amount;
            Currency = currency;
        }

        private Money()
        {
            Currency = Currency.None;
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
