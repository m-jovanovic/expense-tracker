﻿using System.Runtime.Serialization;
using Application.Commands.Infrastructure;
using Domain.Core.Primitives;

namespace Application.Commands.Users.CreateUser
{
    /// <summary>
    /// Represents the command for creating a user.
    /// </summary>
    [DataContract]
    public sealed class CreateUserCommand : ICommand<Result<EntityCreatedResponse>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserCommand"/> class.
        /// </summary>
        public CreateUserCommand()
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
