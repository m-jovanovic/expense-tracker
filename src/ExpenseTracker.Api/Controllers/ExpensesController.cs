using System.Threading.Tasks;
using ExpenseTracker.Application.Expenses.Commands.CreateExpense;
using ExpenseTracker.Domain.Primitives;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers
{
    /// <summary>
    /// Represents the controller for managing the expenses resource.
    /// </summary>
    [Route("api/expenses")]
    public class ExpensesController : ApiControllerBase
    {
        /// <summary>
        /// Creates a new user expense using the provided <see cref="CreateExpenseCommand"/> command.
        /// </summary>
        /// <param name="createExpenseCommand">The create expense command instance.</param>
        /// <returns>A 204 (No Content) if the operation was successful, otherwise a 400 (Bad Request).</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateExpenseForUser([FromBody]CreateExpenseCommand createExpenseCommand)
        {
            Result result = await Mediator.Send(createExpenseCommand);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }
    }
}
