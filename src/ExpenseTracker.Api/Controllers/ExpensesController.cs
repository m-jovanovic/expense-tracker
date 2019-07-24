using System.Threading.Tasks;
using ExpenseTracker.Application.Expenses.Commands.CreateExpense;
using ExpenseTracker.Domain.Primitives;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers
{
    [Route("api/expenses")]
    public class ExpensesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExpensesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpenseForUser([FromBody]CreateExpenseCommand createExpenseCommand)
        {
            Result result = await _mediator.Send(createExpenseCommand);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }
    }
}
