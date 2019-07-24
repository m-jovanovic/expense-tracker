using System.Data;

namespace ExpenseTracker.Application.Abstractions
{
    /// <summary>
    /// Represents an interface for a database connection factory.
    /// </summary>
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// Gets an open database connection.
        /// </summary>
        /// <returns>The open database connection instance.</returns>
        IDbConnection GetOpenConnection();
    }
}
