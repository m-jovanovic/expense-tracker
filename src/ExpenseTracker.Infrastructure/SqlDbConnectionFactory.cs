using System.Data;
using System.Data.SqlClient;
using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Infrastructure;

namespace ExpenseTracker.Infrastructure
{
    public sealed class SqlDbConnectionFactory : IDbConnectionFactory
    {
        private readonly ConnectionString _connectionString;

        public SqlDbConnectionFactory(ConnectionString connectionString)
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
