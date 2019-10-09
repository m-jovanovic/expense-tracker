using AutoMapper;
using ExpenseTracker.Application.Abstractions;

namespace ExpenseTracker.Application.Documents
{
    /// <summary>
    /// Represents the user document.
    /// </summary>
    public sealed class User : IMappable
    {
        public User()
        {
            Id = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
        }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <inheritdoc />
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Aggregates.Users.User, User>();
        }
    }
}
