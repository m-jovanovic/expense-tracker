using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Infrastructure;
using Domain.Aggregates.Users;
using Domain.Events;
using Domain.Primitives;
using MediatR;

namespace Application.Commands.Users.CreateUser
{
    /// <summary>
    /// Represents the command handler for the <see cref="CreateUser"/> command.
    /// </summary>
    public sealed class CreateUserHandler : IRequestHandler<CreateUser, Result<EntityCreatedResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository instance.</param>
        /// <param name="mediator">Tbe mediator instance.</param>
        public CreateUserHandler(IUserRepository userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        /// <inheritdoc />
        public async Task<Result<EntityCreatedResponse>> Handle(CreateUser request, CancellationToken cancellationToken)
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

            User? existingUser = await _userRepository.GetByEmailAsync(email);

            if (!(existingUser is null))
            {
                return Result.Fail<EntityCreatedResponse>("The specified email is already in use.");
            }

            var user = new User(
                Guid.NewGuid(),
                request.FirstName,
                request.LastName,
                email);

            _userRepository.Insert(user);

            await _mediator.Publish(new UserCreated(user), cancellationToken);

            // TODO: Send confirmation email? Decide on this.
            return Result.Ok<EntityCreatedResponse>(user.Id);
        }
    }
}