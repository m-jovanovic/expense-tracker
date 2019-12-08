using System;
using Domain.Core.Events;

namespace Domain.Users.Events
{
    /// <summary>
    /// Represents the event that fires when a user is created.
    /// </summary>
    public sealed class UserCreatedEvent : DomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserCreatedEvent"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="firstName">The first name of the user.</param>
        /// <param name="lastName">The last name of the user.</param>
        /// <param name="email">The email of the user.</param>
        public UserCreatedEvent(Guid userId, string firstName, string lastName, string email)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Gets the first name of the user.
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        /// Gets the last name of the user.
        /// </summary>
        public string LastName { get; }

        /// <summary>
        /// Gets the email of the user.
        /// </summary>
        public string Email { get; }
    }
}
