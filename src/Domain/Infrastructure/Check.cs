using System;
using Domain.Exceptions;
using Domain.Expenses;

namespace Domain.Infrastructure
{
    /// <summary>
    /// Contains assertions for the most common application checks.
    /// </summary>
    public static class Check
    {
        /// <summary>
        /// Checks that the specified <see cref="Guid"/> amount is not empty.
        /// </summary>
        /// <param name="value">The amount to check.</param>
        /// <param name="message">The message to show if the check fails.</param>
        /// <param name="argumentName">The name of the argument being checked.</param>
        /// <exception cref="ArgumentException"> if the specified amount is empty.</exception>
        public static void NotEmpty(Guid value, string message, string argumentName)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException(message, argumentName);
            }
        }

        /// <summary>
        /// Checks that the specified <see cref="string"/> amount is not empty.
        /// </summary>
        /// <param name="value">The amount to check.</param>
        /// <param name="message">The message to show if the check fails.</param>
        /// <param name="argumentName">The name of the argument being checked.</param>
        /// <exception cref="ArgumentException"> if the specified amount is empty.</exception>
        public static void NotEmpty(string value, string message, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(message, argumentName);
            }
        }

        /// <summary>
        /// Checks that the specified <see cref="Currency"/> amount is not empty.
        /// </summary>
        /// <param name="currency">The currency to check.</param>
        /// <param name="message">The message to show if the check fails.</param>
        /// <param name="argumentName">The name of the argument being checked.</param>
        /// <exception cref="ArgumentException"> if the specified currency is empty.</exception>
        public static void NotEmpty(Currency currency, string message, string argumentName)
        {
            if (currency.Equals(Currency.Empty))
            {
                throw new ArgumentException(message, argumentName);
            }
        }

        /// <summary>
        /// Checks that the specified <see cref="Money"/> amount is not empty.
        /// </summary>
        /// <param name="money">The money to check.</param>
        /// <exception cref="EmptyMoneyException"> if the specified money is empty.</exception>
        public static void NotEmpty(Money money)
        {
            if (money == Money.Empty)
            {
                throw new EmptyMoneyException();
            }
        }

        /// <summary>
        /// Checks that the specified <see cref="decimal"/> amount is greater than zero.
        /// </summary>
        /// <param name="amount">The amount to check.</param>
        /// <exception cref="NegativeAmountException"> if the specified amount is less than zero.</exception>
        public static void AmountGreaterThanZero(decimal amount)
        {
            if (amount < decimal.Zero)
            {
                throw new NegativeAmountException(amount);
            }
        }

        /// <summary>
        /// Checks that the specified start date precedes the specified end date.
        /// </summary>
        /// <param name="startDate">The start date to check.</param>
        /// <param name="endDate">The end date to check.</param>
        /// <exception cref="EndDatePrecedesStartDateException"> if the specified end date precedes the start date.</exception>
        public static void StartDatePrecedesEndDate(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                throw new EndDatePrecedesStartDateException(startDate, endDate);
            }
        }
    }
}
