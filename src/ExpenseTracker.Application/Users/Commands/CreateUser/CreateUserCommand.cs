using ExpenseTracker.Domain.Primitives;
using MediatR;

namespace ExpenseTracker.Application.Users.Commands.CreateUser
{
    /// <summary>
    /// Represents the command for creating a user.
    /// </summary>
    public sealed class CreateUserCommand : IRequest<Result>
    {
        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user last name.
        /// </summary>
        public string LastName { get; set; }
    }
}
