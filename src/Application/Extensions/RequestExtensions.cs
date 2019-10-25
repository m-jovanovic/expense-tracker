using Application.Commands;
using Application.Queries;
using MediatR;

namespace Application.Extensions
{
    /// <summary>
    /// Contains extensions methods for the <see cref="IRequest{TResponse}"/> class.
    /// </summary>
    public static class RequestExtensions
    {
        /// <summary>
        /// Returns a boolean value indicating if the request instance is a query.
        /// </summary>
        /// <typeparam name="T">The request type.</typeparam>
        /// <param name="request">The request instance.</param>
        /// <returns>True if the request instance is a query, otherwise false.</returns>
        public static bool IsQuery<T>(this IRequest<T> request)
        {
            return request is IQuery<T> && !(request is ICommand<T>);
        }

        /// <summary>
        /// Returns a boolean value indicating if the request instance is a command.
        /// </summary>
        /// <typeparam name="T">The request type.</typeparam>
        /// <param name="request">The request instance.</param>
        /// <returns>True if the request instance is a command, otherwise false.</returns>
        public static bool IsCommand<T>(this IRequest<T> request)
        {
            return !request.IsQuery();
        }
    }
}
