using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Domain.Aggregates.Users;
using MediatR;

namespace ExpenseTracker.Application.Users.Queries.GetUser
{
    /// <summary>
    /// Represents the handler for the <see cref="GetUserQueryHandler"/> query.
    /// </summary>
    public sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, User?>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserQueryHandler"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The database connection factory instance.</param>
        public GetUserQueryHandler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <inheritdoc />
        public async Task<User?> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                return default;
            }

            const string sql = "SELECT * FROM [User] WHERE Id = @Id";

            using IDbConnection connection = _dbConnectionFactory.GetOpenConnection();

            return await connection.QuerySingleOrDefaultAsync<User>(sql, request);
        }
    }
}