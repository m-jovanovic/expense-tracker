using System;
using ExpenseTracker.Domain.Aggregates.Users;
using ExpenseTracker.Domain.Primitives;
using MediatR;

namespace ExpenseTracker.Application.Users.Queries.GetUser
{
    /// <summary>
    /// Represents the query for getting a user.
    /// </summary>
    public sealed class GetUserQuery : IRequest<Maybe<User>>
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid Id { get; set; }
    }
}
