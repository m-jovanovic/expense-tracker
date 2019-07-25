using System;
using ExpenseTracker.Domain.Primitives;

namespace ExpenseTracker.Domain.Extensions
{
    /// <summary>
    /// Contains extensions methods for the common uses of the <see cref="Result"/> class.
    /// </summary>
    public static class ResultExtensions
    {
        /// <summary>
        /// Converts the specified <see cref="Maybe{T}"/> instance to a <see cref="Result{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="maybe">The maybe instance.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>The result instance with the specified maybe value.</returns>
        public static Result<T> ToResult<T>(this Maybe<T> maybe, string errorMessage)
            where T : class
        {
            return maybe.HasValue ? Result.Ok(maybe.Value) : Result.Fail<T>(errorMessage);
        }

        /// <summary>
        /// Executes the specified function and returns the result if the current result is successful.
        /// </summary>
        /// <typeparam name="T1">The current result type.</typeparam>
        /// <typeparam name="T2">The output result type.</typeparam>
        /// <param name="result">The current result.</param>
        /// <param name="onSuccessFunc">The function that processes the current result and returns a new result.</param>
        /// <returns>The result instance of the function if the current result is successful, otherwise a fail result.</returns>
        public static Result<T2> OnSuccess<T1, T2>(this Result<T1> result, Func<T1, T2> onSuccessFunc)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            if (onSuccessFunc == null)
            {
                return Result.Fail<T2>("The on success function can not be null.");
            }

            return result.IsSuccess ? Result.Ok(onSuccessFunc(result.Value)) : Result.Fail<T2>(result.Error);
        }

        /// <summary>
        /// Ensures that the current result instance satisfies the specified condition, otherwise returns a fail result.
        /// </summary>
        /// <typeparam name="T">The result type.</typeparam>
        /// <param name="result">The current result.</param>
        /// <param name="predicate">The predicate that must be satisfied.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>The current result if it is a failure or satisfies the predicate, otherwise a fail result.</returns>
        public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, string errorMessage)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            if (predicate == null)
            {
                return Result.Fail<T>("The predicate can not be null.");
            }

            if (result.IsFailure)
            {
                return result;
            }

            return predicate(result.Value) ? result : Result.Fail<T>(errorMessage);
        }

        /// <summary>
        /// Maps the result instance to a new result instance using the specified function.
        /// </summary>
        /// <typeparam name="TIn">The current result type.</typeparam>
        /// <typeparam name="TOut">The output result type.</typeparam>
        /// <param name="result">The current result.</param>
        /// <param name="mapFunc">The function that will map the current result to the new result instance.</param>
        /// <returns>The result of the map function is the current result is successful, otherwise a fail result.</returns>
        public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> mapFunc)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            if (mapFunc == null)
            {
                return Result.Fail<TOut>("The map function can not be null.");
            }

            return result.IsSuccess ? Result.Ok(mapFunc(result.Value)) : Result.Fail<TOut>(result.Error);
        }
    }
}