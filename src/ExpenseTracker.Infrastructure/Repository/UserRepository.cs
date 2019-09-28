using System;
using System.Threading.Tasks;
using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Specification;
using ExpenseTracker.Domain.Aggregates.Users;

namespace ExpenseTracker.Infrastructure.Repository
{
    /// <summary>
    /// Represents the user repository.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public UserRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public void InsertUser(User user)
        {
            _dbContext.Insert(user);
        }

        /// <inheritdoc />
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return default;
            }

            return await _dbContext.GetBySpecificationAsync(new UserSpecification(email));
        }

        /// <inheritdoc />
        public async Task<User?> GetUserByIdWithExpensesAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return default;
            }

            return await _dbContext.GetBySpecificationAsync(new UserWithExpensesSpecification(id));
        }
    }
}
