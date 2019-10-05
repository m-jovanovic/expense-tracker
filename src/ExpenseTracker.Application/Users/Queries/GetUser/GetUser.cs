using System;
using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Domain.Aggregates.Users;

namespace ExpenseTracker.Application.Users.Queries.GetUser
{
    /// <summary>
    /// Represents the query for getting a user.
    /// </summary>
    public sealed class GetUser : IQuery<User?>
    {
        public GetUser(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid Id { get; }
    }
}
