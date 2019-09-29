using ExpenseTracker.Domain.Aggregates.Users;

namespace ExpenseTracker.Application.QuerySpecifications
{
    /// <summary>
    /// Represents the specification for the <see cref="User"/> entity.
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
