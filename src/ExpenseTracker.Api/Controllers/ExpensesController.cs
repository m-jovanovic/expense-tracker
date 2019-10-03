using System.Threading.Tasks;
using ExpenseTracker.Application.Expenses.Commands.CreateExpense;
using ExpenseTracker.Application.Expenses.Commands.DeleteExpense;
using ExpenseTracker.Application.Expenses.Queries.GetExpenses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers
{
    /// <summary>
    /// Represents the controller for managing the expenses resource.
    /// </summary>
    [Route("api/expenses")]
    public class ExpensesController : ApiController
    {
        /// <summary>
        /// Creates a new expense using the provided <see cref="CreateExpenseCommand"/> command.
        /// </summary>
        /// <param name="createExpenseCommand">The create expense command instance.</param>
        /// <returns>A 201 (Created) if the operation was successful, otherwise a 400 (Bad Request).</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody]CreateExpenseCommand createExpenseCommand)
        {
            return await ProcessCreateCommandAsync(createExpenseCommand, nameof(Get));
        }

        /// <summary>
        /// Deletes an expense using the provided <see cref="DeleteExpenseCommand"/> command.
        /// </summary>
        /// <param name="deleteExpenseCommand">The delete expense command instance.</param>
        /// <returns>A 204 (No Content) if the operation was successful, otherwise a 400 (Bad Request).</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromBody]DeleteExpenseCommand deleteExpenseCommand)
        {
            return await ProcessDeleteCommandAsync(deleteExpenseCommand);
        }
    }
}
