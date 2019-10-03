using System;
using System.Threading.Tasks;
using ExpenseTracker.Application.Users.Commands.CreateUser;
using ExpenseTracker.Application.Users.Queries.GetUser;
using ExpenseTracker.Domain.Aggregates.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers
{
    /// <summary>
    /// Represents the controller for managing the users resource.
    /// </summary>
    [Route("api/users")]
    public class UsersController : ApiController
    {
        /// <summary>
        /// Gets the user with the specified identifier.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>The user with the specified identifier, it it exists.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            return await ProcessQueryAsync(new GetUserQuery { Id = id });
        }

        /// <summary>
        /// Creates a new user using the provided <see cref="CreateUserCommand"/> command.
        /// </summary>
        /// <param name="createUserCommand">The create user command instance.</param>
        /// <returns>A 201 (Created) if the operation was successful, otherwise a 400 (Bad Request).</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody]CreateUserCommand createUserCommand)
        {
            return await ProcessCreateCommandAsync(createUserCommand, nameof(Get));
        }
    }
}