using System.Runtime.Serialization;
using Application.Abstractions;
using Application.Infrastructure;
using Domain.Primitives;

namespace Application.Users.Commands.CreateUser
{
    /// <summary>
    /// Represents the command for creating a user.
    /// </summary>
    [DataContract]
    public sealed class CreateUser : ICommand<Result<EntityCreatedResponse>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUser"/> class.
        /// </summary>
        public CreateUser()
        {
            Email = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
        }

        /// <summary>
        /// Gets the user email.
        /// </summary>
        [DataMember]
        public string Email { get; private set; }

        /// <summary>
        /// Gets the user first name.
        /// </summary>
        [DataMember]
        public string FirstName { get; private set; }

        /// <summary>
        /// Gets the user last name.
        /// </summary>
        [DataMember]
        public string LastName { get; private set; }
    }
}
