using System;
using System.Threading.Tasks;
using Application.Commands.Users.CreateUser;
using Application.Documents.Documents;
using Application.Queries.Users.GetUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// Represents the controller for managing the users resource.
    /// </summary>
    [Route("api/users")]
    public class UsersController : ApiController
    {
        /// <summary>
        /// Gets the user with the specified identifier, if it exists.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>The user with the specified identifier, it it exists.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id) =>
            await ProcessQueryAndReturnOkAsync(new GetUserQuery(id));

        /// <summary>
        /// Creates a new user using the provided <see cref="CreateUserCommand"/> command.
        /// </summary>
        /// <param name="createUserCommand">The create user command instance.</param>
        /// <returns>A 201 (Created) if the operation was successful, otherwise a 400 (Bad Request).</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody]CreateUserCommand createUserCommand) =>
            await ProcessCommandAndReturnCreatedAsync(createUserCommand, nameof(Get));
    }
}