using System;
using System.Threading.Tasks;
using ExpenseTracker.Application.Users.Commands;
using ExpenseTracker.Application.Users.Commands.CreateUser;
using ExpenseTracker.Application.Users.Queries.GetUser;
using ExpenseTracker.Domain.Aggregates.UserAggregate;
using ExpenseTracker.Domain.Primitives;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            Maybe<User> userOrNothing = await _mediator.Send(new GetUserQuery { Id = id });

            if (userOrNothing.HasNoValue)
            {
                return NotFound();
            }

            return Ok(userOrNothing.Value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand createUserCommand)
        {
            Result result = await _mediator.Send(createUserCommand);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }
    }
}