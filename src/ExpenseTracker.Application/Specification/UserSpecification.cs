using ExpenseTracker.Domain.Aggregates.UserAggregate;

namespace ExpenseTracker.Application.Specification
{
    public sealed class UserSpecification : SpecificationBase<User>
    {
        public UserSpecification(string email) 
            : base(u => u.Email == email)
        {
        }
    }
}
