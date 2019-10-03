using System;
using System.Threading;
using System.Threading.Tasks;
using ExpenseTracker.Application.Infrastructure;
using ExpenseTracker.Domain.Aggregates.Users;
using ExpenseTracker.Domain.Primitives;
using MediatR;

namespace ExpenseTracker.Application.Users.Commands.CreateUser
{
    /// <summary>
    /// Represents the command handler for the <see cref="CreateUserCommand"/> command.
    /// </summary>
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<EntityCreatedResponse>>
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserCommandHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository instance.</param>
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <inheritdoc />
        public async Task<Result<EntityCreatedResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Result<Email> emailResult = Email.Create(request.Email);

            if (emailResult.IsFailure)
            {
                return Result.Fail<EntityCreatedResponse>(emailResult.Error);
            }

            Email? email = emailResult.Value();

            if (email is null || string.IsNullOrWhiteSpace(email))
            {
                return Result.Fail<EntityCreatedResponse>("The specified email is invalid.");
            }

            User? existingUser = await _userRepository.GetUserByEmailAsync(email);

            if (!(existingUser is null))
            {
                return Result.Fail<EntityCreatedResponse>("The specified email is already in use.");
            }

            var user = new User(
                Guid.NewGuid(),
                request.FirstName,
                request.LastName,
                email);

            _userRepository.InsertUser(user);

            // TODO: Send confirmation email.
            return Result.Ok<EntityCreatedResponse>(user.Id);
        }
    }
}