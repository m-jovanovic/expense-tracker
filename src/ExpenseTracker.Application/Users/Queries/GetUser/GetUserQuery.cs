using System;
using ExpenseTracker.Domain.Aggregates.UserAggregate;
using ExpenseTracker.Domain.Primitives;
using MediatR;

namespace ExpenseTracker.Application.Users.Queries.GetUser
{
    public class GetUserQuery : IRequest<Maybe<User>>
    {
        public Guid Id { get; set; }
    }
}
