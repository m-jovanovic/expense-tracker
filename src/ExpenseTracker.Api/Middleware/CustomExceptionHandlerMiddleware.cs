using System;
using System.Net;
using System.Threading.Tasks;
using ExpenseTracker.Application.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ExpenseTracker.Api.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var httpStatusCode = HttpStatusCode.InternalServerError;

            string result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    httpStatusCode = HttpStatusCode.BadRequest;

                    context.Response.ContentType = "application/json";

                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                    result = JsonConvert.SerializeObject(validationException.Failures);

                    break;
                case EntityNotFoundException _:
                    httpStatusCode = HttpStatusCode.NotFound;

                    break;
            }

            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)httpStatusCode;

            if (result.Length == 0)
            {
                result = JsonConvert.SerializeObject(new
                {
                    error = exception.Message
                });
            }

            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}