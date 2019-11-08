using Domain.Core.Primitives;

namespace Domain.Expenses
{
    /// <summary>
    /// Represents a currency.
    /// </summary>
    public sealed class Currency : Enumeration
    {
        public static readonly Currency Empty = new Currency(default, string.Empty, string.Empty);
        public static readonly Currency Rsd = new Currency(1, "Serbian Dinar", "RSD");
        public static readonly Currency Eur = new Currency(2, "Euro", "€");
        public static readonly Currency Usd = new Currency(3, "Dollar", "$");

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

        /// <summary>
        /// Initializes a new instance of the <see cref="Currency"/> class.
        /// </summary>
        private Currency()
        {
            Symbol = string.Empty;
        }

        /// <summary>
        /// Gets the currency symbol.
        /// </summary>
        public string Symbol { get; private set; }
    }
}
