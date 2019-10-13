using System;
using System.Threading.Tasks;

namespace Domain.Aggregates.Users
{
    /// <summary>
    /// Represents the user repository interface.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Inserts the specified user to the database.
        /// </summary>
        /// <param name="user">The user to insert.</param>
        void Insert(User user);

        /// <summary>
        /// Gets the user with the specified identifier, if one exists.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>The user with the specified identifier it it exists, otherwise null.</returns>
        Task<User?> GetByIdAsync(Guid id);

        /// <summary>
        /// Gets the user with the specified email, if such a user exists.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns>The user if it is found, otherwise null.</returns>
        Task<User?> GetByEmailAsync(string email);
    }
}
