using System;
using System.Threading.Tasks;
using ExpenseTracker.Application.Users.Commands.CreateUser;
using ExpenseTracker.Application.Users.Queries.GetUser;
using ExpenseTracker.Domain.Aggregates.Users;
using ExpenseTracker.Domain.Primitives;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers
{
    /// <summary>
    /// Represents the controller for managing the users resource.
    /// </summary>
    [Route("api/users")]
    public class UsersController : ApiControllerBase
    {
        /// <summary>
        /// Gets the user with the specified identifier.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>The user with the specified identifier, it it exists.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(Guid id)
        {
            Maybe<User> userOrNothing = await Mediator.Send(new GetUserQuery { Id = id });

            if (userOrNothing.HasNoValue)
            {
                return NotFound();
            }

            return Ok(userOrNothing.Value);
        }

        /// <summary>
        /// Creates a new user using the provided <see cref="CreateUserCommand"/> command.
        /// </summary>
        /// <param name="createUserCommand">The create user command instance.</param>
        /// <returns>A 204 (No Content) if the operation was successful, otherwise a 400 (Bad Request).</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserCommand createUserCommand)
        {
            Result result = await Mediator.Send(createUserCommand);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }
    }
}