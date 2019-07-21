﻿using System.Threading.Tasks;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Primitives;

namespace ExpenseTracker.Domain.Aggregates.UserAggregate
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Inserts the specified user to the database.
        /// </summary>
        /// <param name="user">The user.</param>
        void InsertUser(User user);

        /// <summary>
        /// Gets the user with the specified email, if such a user exists.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns>The user if it is found, otherwise null.</returns>
        Task<Maybe<User>> GetUserByEmailAsync(string email);
    }
}