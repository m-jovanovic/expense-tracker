using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Domain.Aggregates.UserAggregate;
using ExpenseTracker.Domain.Primitives;
using MediatR;

namespace ExpenseTracker.Application.Users.Queries.GetUser
{
    public class  GetUserQueryHandler : IRequestHandler<GetUserQuery, Maybe<User>>
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public GetUserQueryHandler(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Maybe<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                return null;
            }

            const string sql = "SELECT * FROM [User] WHERE Id = @Id";

            using (IDbConnection connection = _connectionFactory.GetOpenConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<User>(sql, request);
            }
        }
    }
}