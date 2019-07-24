using System;
using MediatR;

namespace ExpenseTracker.Application.Extensions
{
    /// <summary>
    /// Contains extensions methods for the <see cref="IRequest{TResponse}"/> class.
    /// </summary>
    public static class RequestExtensions
    {
        /// <summary>
        /// Returns a boolean value indicating if the request instance is a command.
        /// </summary>
        /// <typeparam name="T">The request type.</typeparam>
        /// <param name="request">The request instance.</param>
        /// <returns>True if the request instance is a command, otherwise false.</returns>
        public static bool IsCommand<T>(this IRequest<T> request)
        {
            return request.GetType().Name.EndsWith("Command", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
