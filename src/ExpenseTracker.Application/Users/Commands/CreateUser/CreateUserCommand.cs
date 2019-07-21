using ExpenseTracker.Domain.Primitives;
using MediatR;

namespace ExpenseTracker.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Result>
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
