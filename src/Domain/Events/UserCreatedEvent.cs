using Domain.Aggregates.Users;
using Domain.Core.Events;

namespace Domain.Events
{
    /// <summary>
    /// Represents the event that fires when a user is created.
    /// </summary>
    public sealed class UserCreatedEvent : BaseDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserCreatedEvent"/> class.
        /// </summary>
        /// <param name="user">The user that was created.</param>
        public UserCreatedEvent(User user)
        {
            User = user;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        public User User { get; }
    }
}
