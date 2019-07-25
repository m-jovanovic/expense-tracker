using ExpenseTracker.Domain.Primitives;

namespace ExpenseTracker.Domain.Aggregates.ExpenseAggregate
{
    /// <summary>
    /// Represents a currency.
    /// </summary>
    public sealed class Currency : Enumeration
    {
        public static readonly Currency Rsd = new Currency(1, "Serbian Dinar", "RSD");
        public static readonly Currency Eur = new Currency(2, "Euro", "€");

        /// <summary>
        /// Initializes a new instance of the <see cref="Currency"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="name">The name.</param>
        /// <param name="symbol">The symbol.</param>
        private Currency(int value, string name, string symbol)
            : base(value, name)
        {
            Symbol = symbol;
        }

        private Currency()
        {
        }

        /// <summary>
        /// Gets the currency symbol.
        /// </summary>
        public string Symbol { get; private set; }
    }
}
