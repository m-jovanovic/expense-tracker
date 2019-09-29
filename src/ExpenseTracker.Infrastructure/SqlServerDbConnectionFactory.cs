using System.Data;
using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Infrastructure;
using Microsoft.Data.SqlClient;

namespace ExpenseTracker.Infrastructure
{
    /// <summary>
    /// Represents the SQL Server database connection factory.
    /// </summary>
    internal class SqlServerDbConnectionFactory : IDbConnectionFactory
    {
        private readonly ConnectionString _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServerDbConnectionFactory"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public SqlServerDbConnectionFactory(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        /// <inheritdoc />
        public IDbConnection GetOpenConnection()
        {
            var connection = new SqlConnection(_connectionString);

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            return connection;
        }
    }
}
