using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Api.Controllers
{
    /// <summary>
    /// Represents the base API controller.
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
    }
}
