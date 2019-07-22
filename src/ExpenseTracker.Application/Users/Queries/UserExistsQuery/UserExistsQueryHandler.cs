using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using ExpenseTracker.Application.Infrastructure;
using MediatR;

namespace ExpenseTracker.Application.Users.Queries.UserExistsQuery
{
    public sealed class UserExistsQueryHandler : IRequestHandler<UserExistsQuery, bool>
    {
        private readonly ConnectionString _connectionString;

        public UserExistsQueryHandler(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public async  Task<bool> Handle(UserExistsQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                return false;
            }

            const string sql = "SELECT TOP 1 1 FROM [User] WHERE Id = @Id";

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                return await sqlConnection.ExecuteScalarAsync<bool>(sql, request);
            }
        }
    }
}