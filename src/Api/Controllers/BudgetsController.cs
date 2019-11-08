using System.Threading.Tasks;
using Application.Commands.Budgets.CreateBudget;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// Represents the controller for managing the budgets resource.
    /// </summary>
    [Route("api/budgets")]
    public class BudgetsController : ApiController
    {
        // TODO: Implement Get action.

        /// <summary>
        /// Creates a new budget using the provided <see cref="CreateBudgetCommand"/> command.
        /// </summary>
        /// <param name="createBudgetCommand">The create budget command instance.</param>
        /// <returns>A 201 (Created) if the operation was successful, otherwise a 400 (Bad Request).</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody]CreateBudgetCommand createBudgetCommand) =>
            await ProcessCommandAndReturnCreatedAsync(createBudgetCommand, "Get");
    }
}
