using System;
using Domain.Core.Primitives;
using Domain.Infrastructure;

namespace Domain.Users
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
        /// <exception cref="ArgumentException"> if any of the parameters is either null or empty.</exception>
        public User(Guid id, string firstName, string lastName, Email email)
        {
            Ensure.NotEmpty(id, "The identifier is required", nameof(id));
            Ensure.NotEmpty(firstName, "The first name is required", nameof(firstName));
            Ensure.NotEmpty(lastName, "The last name is required", nameof(lastName));
            Ensure.NotEmpty(email, "The email is required", nameof(email));

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
