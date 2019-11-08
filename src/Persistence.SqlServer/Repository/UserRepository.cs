using System;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Users;
using Persistence.SqlServer.QuerySpecifications;

namespace Persistence.SqlServer.Repository
{
    /// <summary>
    /// Represents the user repository.
    /// </summary>
    internal sealed class UserRepository : IUserRepository
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public UserRepository(IDbContext dbContext) => _dbContext = dbContext;

        /// <inheritdoc />
        public void Insert(User user) => _dbContext.Insert(user);

        /// <inheritdoc />
        public async Task<User?> GetByIdAsync(Guid id) => await _dbContext.GetByIdAsync<User>(id);

        /// <inheritdoc />
        public async Task<User?> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return default;
            }

            return await _dbContext.GetByQuerySpecificationAsync(new UserQuerySpecification(email));
        }
    }
}
