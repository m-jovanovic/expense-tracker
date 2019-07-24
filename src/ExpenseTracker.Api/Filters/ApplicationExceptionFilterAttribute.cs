using System;
using System.Net;
using ExpenseTracker.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ValidationException = ExpenseTracker.Application.Exceptions.ValidationException;

namespace ExpenseTracker.Api.Filters
{
    /// <summary>
    /// Represents an application exception filter that controls what response the API returns based on the thrown exception.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApplicationExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <inheritdoc />
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException exception)
            {
                context.HttpContext.Response.ContentType = "application/json";

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                context.Result = new JsonResult(exception.Failures);

                return;
            }

            var code = HttpStatusCode.InternalServerError;

            if (context.Exception is EntityNotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }

            context.HttpContext.Response.ContentType = "application/json";

            context.HttpContext.Response.StatusCode = (int)code;

            context.Result = new JsonResult(new
            {
                error = new[] { context.Exception.Message },
                stackTrace = context.Exception.StackTrace
            });
        }
    }
}
