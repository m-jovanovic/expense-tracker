using System;
using System.Threading;
using System.Threading.Tasks;
using ExpenseTracker.Domain.Aggregates.UserAggregate;
using ExpenseTracker.Domain.Primitives;
using MediatR;

namespace ExpenseTracker.Application.Users.Commands.CreateUser
{
    /// <summary>
    /// Represents the command handler for the <see cref="CreateUserCommand"/> command.
    /// </summary>
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
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
        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Result<Email> emailResult = Email.Create(request.Email);

            if (emailResult.IsFailure)
            {
                return  Result.Fail(emailResult.Error);
            }

            Email email = emailResult.Value;

            Maybe<User> existingUserOrNothing = await _userRepository.GetUserByEmailAsync(email);

            if (existingUserOrNothing.HasValue)
            {
                return Result.Fail("The specified email is already in use.");
            }

            var user = new User(Guid.NewGuid(),
                request.FirstName,
                request.LastName,
                email);

            _userRepository.InsertUser(user);

            //TODO: Send confirmation email.

            return Result.Ok();
        }
    }
}