using System;
using System.Net;
using System.Threading.Tasks;
using Application.Commands.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Api.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occurred: {Message}", ex.Message);

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