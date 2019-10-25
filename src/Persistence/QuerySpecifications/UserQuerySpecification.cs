using Application.QuerySpecifications;
using Domain.Aggregates.Users;

namespace Persistence.QuerySpecifications
{
    /// <summary>
    /// Represents the query specification for the <see cref="User"/> entity.
    /// </summary>
    public sealed class UserQuerySpecification : QuerySpecificationBase<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserQuerySpecification"/> class.
        /// </summary>
        /// <param name="email">The email that will be used for creating the filter criteria.</param>
        public UserQuerySpecification(string email)
            : base(u => u.Email.Value == email)
        {
        }
    }
}
