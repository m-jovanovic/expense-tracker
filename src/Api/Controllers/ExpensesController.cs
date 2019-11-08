using System;
using System.Threading.Tasks;
using Application.Commands.Expenses.CreateExpense;
using Application.Commands.Expenses.DeleteExpense;
using Application.Commands.Expenses.UpdateExpense;
using Application.Documents.Documents;
using Application.Queries.Expenses.GetExpense;
using Application.Queries.Expenses.GetExpenses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// Represents the controller for managing the expenses resource.
    /// </summary>
    [Route("api/expenses")]
    public class ExpensesController : ApiController
    {
        /// <summary>
        /// Gets the expense that match the specified query.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The expense with the specified identifier, it it exists.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(Expense), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetExpenses(Guid userId) =>
            await ProcessQueryAndReturnOkAsync(new GetExpensesForUserQuery(userId));

        /// <summary>
        /// Gets the expense with the specified identifier, if it exists.
        /// </summary>
        /// <param name="id">The expense identifier.</param>
        /// <returns>The expense with the specified identifier, it it exists.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Expense), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id) =>
            await ProcessQueryAndReturnOkAsync(new GetExpenseQuery(id));

        /// <summary>
        /// Creates a new expense using the provided <see cref="CreateExpenseCommand"/> command.
        /// </summary>
        /// <param name="createExpenseCommand">The create expense command instance.</param>
        /// <returns>A 201 (Created) if the operation was successful, otherwise a 400 (Bad Request).</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody]CreateExpenseCommand createExpenseCommand) =>
            await ProcessCommandAndReturnCreatedAsync(createExpenseCommand, nameof(Get));

        /// <summary>
        /// Updates the expense using the provided <see cref="UpdateExpenseCommand"/> command.
        /// </summary>
        /// <param name="updateExpenseCommand">The update expense command instance.</param>
        /// <returns>A 204 (No Content) if the operation was successful, otherwise a 400 (Bad Request).</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody]UpdateExpenseCommand updateExpenseCommand) =>
            await ProcessCommandAndReturnNoContentAsync(updateExpenseCommand);

        /// <summary>
        /// Deletes an expense using the provided <see cref="DeleteExpenseCommand"/> command.
        /// </summary>
        /// <param name="deleteExpenseCommand">The delete expense command instance.</param>
        /// <returns>A 204 (No Content) if the operation was successful, otherwise a 400 (Bad Request).</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromBody]DeleteExpenseCommand deleteExpenseCommand) =>
            await ProcessCommandAndReturnNoContentAsync(deleteExpenseCommand);
    }
}
