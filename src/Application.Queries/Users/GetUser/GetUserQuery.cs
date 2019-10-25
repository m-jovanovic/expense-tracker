using System;
using Application.Documents.Documents;

namespace Application.Queries.Users.GetUser
{
    /// <summary>
    /// Represents the query for getting a user.
    /// </summary>
    public sealed class GetUserQuery : IQuery<User?>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserQuery"/> class.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        public GetUserQuery(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid Id { get; }
    }
}
