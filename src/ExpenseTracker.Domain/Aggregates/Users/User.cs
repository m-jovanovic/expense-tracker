using System;
using ExpenseTracker.Domain.Primitives;

namespace ExpenseTracker.Domain.Aggregates.Users
{
    /// <summary>
    /// Represents the user entity.
    /// </summary>
    public sealed class User : AggregateRoot, IAuditableEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="firstName">The user first name.</param>
        /// <param name="lastName">The user last name.</param>
        /// <param name="email">The user email.</param>
        public User(Guid id, string firstName, string lastName, Email email)
            : this()
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        private User()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = Email.Empty;
        }

        /// <summary>
        /// Gets the first name.
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Gets the last name.
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Gets the email.
        /// </summary>
        public Email Email { get; private set; }

        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; private set; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; private set; }
    }
}
