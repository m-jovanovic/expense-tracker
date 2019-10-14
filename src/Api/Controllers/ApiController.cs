using System.Threading.Tasks;
using Application.Commands;
using Application.Commands.Infrastructure;
using Application.Queries;
using Domain.Primitives;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Controllers
{
    /// <summary>
    /// Represents the base API controller that all controllers should inherit.
    /// </summary>
    public abstract class ApiController : ControllerBase
    {
        // TODO: Remove pragma when [AllowNull] attribute starts working.
        #nullable disable
        private IMediator _mediator;
        #nullable enable

        /// <summary>
        /// Gets the <see cref="IMediator"/> instance.
        /// </summary>
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        /// <summary>
        /// Processes the specified query.
        /// </summary>
        /// <param name="request">The request instance.</param>
        /// <typeparam name="TValue">The response value type.</typeparam>
        /// <returns>A 200 (OK) if the request resource was found, otherwise a 404 (Not Found).</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        protected virtual async Task<IActionResult> ProcessQueryAndReturnOkAsync<TValue>(IQuery<TValue?> request)
            where TValue : class
        {
            TValue? response = await Mediator.Send(request);

            if (response is null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        /// <summary>
        /// Processes the specified create command.
        /// </summary>
        /// <param name="request">The request instance.</param>
        /// <param name="actionName">The name of the action to get the resource from.</param>
        /// <returns>A 201 (Created) if the operation was successful, otherwise a 400 (Bad Request).</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        protected virtual async Task<IActionResult> ProcessCommandAndReturnCreatedAsync(ICommand<Result<EntityCreatedResponse>> request, string actionName)
        {
            Result<EntityCreatedResponse> result = await Mediator.Send(request);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(actionName, new { id = result.Value()?.Id }, null);
        }

        /// <summary>
        /// Processes the specified request.
        /// </summary>
        /// <param name="request">The request instance.</param>
        /// <returns>A 204 (No Content) if the operation was successful, otherwise a 400 (Bad Request).</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        protected async Task<IActionResult> ProcessCommandAndReturnNoContentAsync(ICommand<Result> request)
        {
            Result result = await Mediator.Send(request);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }
    }
}
