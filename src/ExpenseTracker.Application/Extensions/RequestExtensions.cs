using System;
using MediatR;

namespace ExpenseTracker.Application.Extensions
{
    public static class RequestExtensions
    {
        public static bool IsCommand<T>(this IRequest<T> request)
        {
            return request.GetType().Name.EndsWith("Command", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
