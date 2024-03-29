﻿using System;
using System.Collections.Generic;
using Domain.Core.Primitives;
using Domain.Exceptions;
using Domain.Infrastructure;

namespace Domain.Expenses
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
            Check.AmountGreaterThanZero(amount);
            Check.NotEmpty(currency, "The currency is required", nameof(currency));

            Amount = amount;
            Currency = currency;
        }

        private Money()
        {
            Currency = Currency.Empty;
        }

        public static Money operator +(Money money1, Money money2)
        {
            if (money1.Currency.Equals(money2.Currency))
            {
                throw new InvalidOperationException();
            }

            return new Money(money1.Amount + money2.Amount, money1.Currency);
        }

        public static Money operator -(Money money1, Money money2)
        {
            if (money1.Currency.Equals(money2.Currency) || money1.Amount < money2.Amount)
            {
                throw new InvalidOperationException();
            }

            return new Money(money1.Amount - money2.Amount, money1.Currency);
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
