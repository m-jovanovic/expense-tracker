using Domain.Aggregates.Users;
using MediatR;

namespace Application.Users.Events
{
    /// <summary>
    /// Represents the event that fires when a user is created.
    /// </summary>
    public sealed class UserCreated : INotification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserCreated"/> class.
        /// </summary>
        /// <param name="user">The user that was created.</param>
        public UserCreated(User user)
        {
            User = user;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        public User User { get; }
    }
}
