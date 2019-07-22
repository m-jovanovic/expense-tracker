using System;
using System.Threading.Tasks;
using ExpenseTracker.Application.Expenses.Commands.CreateExpense;
using ExpenseTracker.Application.Users.Queries.UserExistsQuery;
using ExpenseTracker.Domain.Primitives;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers
{
    [Route("api/users/{userId}/expenses")]
    public class ExpensesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExpensesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpenseForUser(Guid userId, [FromBody]CreateExpenseCommand createExpenseCommand)
        {
            bool userExists = await _mediator.Send(new UserExistsQuery { Id = userId });

            if (!userExists)
            {
                return NotFound();
            }

            Result result = await _mediator.Send(createExpenseCommand);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }
    }
}
