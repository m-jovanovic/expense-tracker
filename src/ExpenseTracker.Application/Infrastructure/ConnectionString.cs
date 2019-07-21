namespace ExpenseTracker.Application.Infrastructure
{
    /// <summary>
    /// Represents a database connection string.
    /// </summary>
    public sealed class ConnectionString
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionString"/> class.
        /// </summary>
        /// <param name="value">The connection string value.</param>
        public ConnectionString(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the connection string value.
        /// </summary>
        public string Value { get; }

        public static implicit operator string(ConnectionString connectionString) => connectionString.Value;
    }
}
