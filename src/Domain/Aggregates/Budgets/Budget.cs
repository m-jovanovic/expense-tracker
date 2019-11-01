using System;
using Domain.Aggregates.Expenses;
using Domain.Core.Primitives;
using Domain.Events;
using Domain.Exceptions;

namespace Domain.Aggregates.Budgets
{
    /// <summary>
    /// Represents the budget entity.
    /// </summary>
    public sealed class Budget : AggregateRoot, IAuditableEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Budget"/> class.
        /// </summary>
        /// <param name="amount">The amount of amount for the budget.</param>
        /// <param name="startsOnUtc">The date and time the budget starts on in UTC format.</param>
        /// <param name="endsOnUtc">The date and time the budget ends on in UTC format.</param>
        /// <exception cref="ArgumentException"> if amount is empty.</exception>
        /// <exception cref="EndDatePrecedesStartDateException"> if end date precedes start date.</exception>
        public Budget(Money amount, DateTime startsOnUtc, DateTime endsOnUtc)
            : this()
        {
            if (amount == Money.Empty)
            {
                throw new ArgumentException("The amount is required", nameof(amount));
            }

            if (endsOnUtc < startsOnUtc)
            {
                throw new EndDatePrecedesStartDateException(startsOnUtc, endsOnUtc);
            }

            Amount = amount;
            StartsOnUtc = startsOnUtc;
            EndsOnUtc = endsOnUtc;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Budget"/> class.
        /// </summary>
        private Budget()
        {
            Amount = Money.Empty;
            Spent = Money.Empty;
        }

        /// <summary>
        /// Gets the amount of amount for the budget.
        /// </summary>
        public Money Amount { get; private set; }

        /// <summary>
        /// Gets the amount of amount spent from the budget.
        /// </summary>
        public Money Spent { get; private set; }

        /// <summary>
        /// Gets the remaining amount of amount from the budget.
        /// </summary>
        public Money Remaining => Amount - Spent;

        /// <summary>
        /// Gets the date and time the budget starts on in UTC format.
        /// </summary>
        public DateTime StartsOnUtc { get; }

        /// <summary>
        /// Gets the date and time the budget ends on in UTC format.
        /// </summary>
        public DateTime EndsOnUtc { get; }

        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; private set; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; private set; }

        /// <summary>
        /// Creates the budget for the current month using the specified parameters.
        /// </summary>
        /// <param name="amount">The amount of amount for the budget.</param>
        /// <param name="utcNow">The current date and time in UTC format.</param>
        /// <returns>The budget for the current month with the specified amount and currency.</returns>
        public static Budget CreateForCurrentMonth(Money amount, DateTime utcNow)
        {
            var startsOn = new DateTime(utcNow.Year, utcNow.Month, 1);

            DateTime endsOn = startsOn.AddMonths(1).AddDays(-1);

            return new Budget(amount, startsOn, endsOn);
        }

        /// <summary>
        /// Withdraws the specified amount of money from the budget.
        /// </summary>
        /// <param name="amount">The amount to withdraw.</param>
        public void Withdraw(Money amount)
        {
            Spent += amount;

            AddDomainEvent(new BudgetAmountWithdrawn(Id));
        }

        /// <summary>
        /// Deposits the specified amount of money to the budget.
        /// </summary>
        /// <param name="amount">The amount of amount to deposit.</param>
        public void Deposit(Money amount)
        {
            Spent -= amount;

            AddDomainEvent(new BudgetAmountDeposited(Id));
        }
    }
}
