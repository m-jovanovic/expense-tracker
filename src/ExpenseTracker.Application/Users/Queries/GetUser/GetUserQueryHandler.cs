using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using ExpenseTracker.Application.Infrastructure;
using ExpenseTracker.Domain.Aggregates.UserAggregate;
using ExpenseTracker.Domain.Primitives;
using MediatR;

namespace ExpenseTracker.Application.Users.Queries.GetUser
{
    public class  GetUserQueryHandler : IRequestHandler<GetUserQuery, Maybe<User>>
    {
        private readonly ConnectionString _connectionString;

        public GetUserQueryHandler(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Maybe<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                return null;
            }

            const string sql = "SELECT * FROM [User] WHERE Id = @Id";

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<User>(sql, request);
            }
        }
    }
}