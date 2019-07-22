using System;
using MediatR;

namespace ExpenseTracker.Application.Users.Queries.UserExistsQuery
{
    public sealed class UserExistsQuery : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
