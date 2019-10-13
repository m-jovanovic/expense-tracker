using System;
using System.Threading.Tasks;
using Application.Documents;
using Application.Expenses.Commands.CreateExpense;
using Application.Expenses.Commands.DeleteExpense;
using Application.Expenses.Commands.UpdateExpense;
using Application.Expenses.Queries.GetExpense;
using Application.Expenses.Queries.GetExpenses;
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
        public async Task<IActionResult> GetExpenses(Guid userId) => await ProcessQueryAndReturnOkAsync(new GetExpensesForUser(userId));

        /// <summary>
        /// Gets the expense with the specified identifier, if it exists.
        /// </summary>
        /// <param name="id">The expense identifier.</param>
        /// <returns>The expense with the specified identifier, it it exists.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Expense), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id) => await ProcessQueryAndReturnOkAsync(new GetExpense(id));

        /// <summary>
        /// Creates a new expense using the provided <see cref="CreateExpense"/> command.
        /// </summary>
        /// <param name="createExpense">The create expense command instance.</param>
        /// <returns>A 201 (Created) if the operation was successful, otherwise a 400 (Bad Request).</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody]CreateExpense createExpense) => await ProcessCommandAndReturnCreatedAsync(createExpense, nameof(Get));

        /// <summary>
        /// Updates the expense using the provided <see cref="UpdateExpense"/> command.
        /// </summary>
        /// <param name="updateExpense">The update expense command instance.</param>
        /// <returns>A 204 (No Content) if the operation was successful, otherwise a 400 (Bad Request).</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody]UpdateExpense updateExpense) => await ProcessCommandAndReturnNoContentAsync(updateExpense);

        /// <summary>
        /// Deletes an expense using the provided <see cref="DeleteExpense"/> command.
        /// </summary>
        /// <param name="deleteExpense">The delete expense command instance.</param>
        /// <returns>A 204 (No Content) if the operation was successful, otherwise a 400 (Bad Request).</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromBody]DeleteExpense deleteExpense) => await ProcessCommandAndReturnNoContentAsync(deleteExpense);
    }
}
