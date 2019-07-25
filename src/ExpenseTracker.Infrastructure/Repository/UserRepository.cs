using System;
using System.Threading.Tasks;
using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Infrastructure;
using ExpenseTracker.Application.Specification;
using ExpenseTracker.Domain.Aggregates.UserAggregate;
using ExpenseTracker.Domain.Primitives;

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
        public async Task<Maybe<User>> GetByIdAsync(Guid id)
        {
            return await _dbContext.GetByIdAsync<User>(id);
        }

        /// <inheritdoc />
        public void InsertUser(User user)
        {
            Check.NotNull(user, nameof(user));

            _dbContext.Insert(user);
        }

        /// <inheritdoc />
        public async Task<Maybe<User>> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            return await _dbContext.GetBySpecificationAsync(new UserSpecification(email));
        }

        /// <inheritdoc />
        public async Task<Maybe<User>> GetUserWithExpensesByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            return await _dbContext.GetBySpecificationAsync(new UserWithExpensesSpecification(id));
        }
    }
}
