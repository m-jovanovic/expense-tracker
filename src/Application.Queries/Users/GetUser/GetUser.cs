using System;
using Application.Documents.Documents;

namespace Application.Queries.Users.GetUser
{
    /// <summary>
    /// Represents the query for getting a user.
    /// </summary>
    public sealed class GetUser : IQuery<User?>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUser"/> class.
        /// </summary>
        /// <param name="id">The user identifier.</param>
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
