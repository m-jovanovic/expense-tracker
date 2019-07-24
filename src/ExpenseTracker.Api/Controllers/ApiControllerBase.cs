using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Api.Controllers
{
    /// <summary>
    /// Represents the base API controller.
    /// </summary>
    public abstract class ApiControllerBase : ControllerBase
    {
        private IMediator _mediator;

        /// <summary>
        /// Gets the <see cref="IMediator"/> instance.
        /// </summary>
        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}
