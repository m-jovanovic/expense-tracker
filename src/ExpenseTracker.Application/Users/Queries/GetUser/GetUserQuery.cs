using System;
using ExpenseTracker.Domain.Aggregates.Users;
using MediatR;

namespace ExpenseTracker.Application.Users.Queries.GetUser
{
    /// <summary>
    /// Represents the query for getting a user.
    /// </summary>
    public sealed class GetUserQuery : IRequest<User?>
    {
        public GetUserQuery(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid Id { get; }
    }
}
