using System;
using System.Threading.Tasks;
using ExpenseTracker.Application.Expenses.Commands.CreateExpense;
using ExpenseTracker.Application.Expenses.Commands.DeleteExpense;
using ExpenseTracker.Application.Expenses.Queries.GetExpense;
using ExpenseTracker.Domain.Aggregates.Expenses;
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
        public async Task<IActionResult> Create([FromBody]CreateExpense createExpense)
            => await ProcessCommandAndReturnCreatedAsync(createExpense, nameof(Get));

        /// <summary>
        /// Deletes an expense using the provided <see cref="DeleteExpense"/> command.
        /// </summary>
        /// <param name="deleteExpense">The delete expense command instance.</param>
        /// <returns>A 204 (No Content) if the operation was successful, otherwise a 400 (Bad Request).</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromBody]DeleteExpense deleteExpense)
            => await ProcessCommandAndReturnNoContentAsync(deleteExpense);
    }
}
