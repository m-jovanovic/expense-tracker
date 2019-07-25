using ExpenseTracker.Domain.Aggregates.UserAggregate;

namespace ExpenseTracker.Application.Specification
{
    /// <summary>
    /// Represents the specification for the <see cref="User"/> entity.
    /// </summary>
    public sealed class UserSpecification : SpecificationBase<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSpecification"/> class.
        /// </summary>
        /// <param name="email">The email that will be used for creating the filter criteria.</param>
        public UserSpecification(string email)
            : base(u => u.Email == email)
        {
        }
    }
}
