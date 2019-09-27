using System;

namespace ExpenseTracker.Domain.Primitives
{
    /// <summary>
    /// Represents a result of some operation, with status information and an error message.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class with the specified parameters.
        /// </summary>
        /// <param name="isSuccess">The flag indicating if the result is successful.</param>
        /// <param name="error">The error message.</param>
        protected Result(bool isSuccess, string error)
        {
            if (isSuccess && !string.IsNullOrWhiteSpace(error))
            {
                throw new InvalidOperationException();
            }

            if (!isSuccess && string.IsNullOrWhiteSpace(error))
            {
                throw new InvalidOperationException();
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        /// <summary>
        /// Gets a value indicating whether the result is a success result.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Gets a value indicating whether the result is a failure result.
        /// </summary>
        public bool IsFailure => !IsSuccess;

        /// <summary>
        /// Gets the error string.
        /// </summary>
        public string Error { get; }

        /// <summary>
        /// Returns a success <see cref="Result"/>.
        /// </summary>
        /// <returns>A new instance of <see cref="Result"/> with the success flag set.</returns>
        public static Result Ok() => new Result(true, string.Empty);

        /// <summary>
        /// Returns a success <see cref="Result"/> with the specified value.
        /// </summary>
        /// <typeparam name="TValue">The result type.</typeparam>
        /// <param name="value">The result value.</param>
        /// <returns>A new instance of <see cref="Result"/> with the success flag set.</returns>
        public static Result<TValue> Ok<TValue>(TValue? value)
            where TValue : class
            => new Result<TValue>(value, true, string.Empty);

        /// <summary>
        /// Returns a fail <see cref="Result"/> with the specified error message.
        /// </summary>
        /// <param name="error">The error message.</param>
        /// <returns>A new instance of <see cref="Result"/> with the specified error message and failure flag set.</returns>
        public static Result Fail(string error) => new Result(false, error);

        /// <summary>
        /// Returns a fail <see cref="Result{T}"/> with the specified error message.
        /// </summary>
        /// <typeparam name="TValue">The result type.</typeparam>
        /// <param name="error">The error message.</param>
        /// <returns>A new instance of <see cref="Result{T}"/> with the specified error message and failure flag set.</returns>
        public static Result<TValue> Fail<TValue>(string error)
            where TValue : class
            => new Result<TValue>(default, false, error);

        /// <summary>
        /// Combines multiple <see cref="Result"/> instances, returning the first failure or a success result.
        /// </summary>
        /// <param name="results">The result instances to combine.</param>
        /// <returns>The first failure <see cref="Result"/> instance or a new success <see cref="Result"/> instance.</returns>
        public static Result FirstFailureOrSuccess(params Result[] results)
        {
            foreach (Result result in results)
            {
                if (result.IsFailure)
                {
                    return result;
                }
            }

            return Ok();
        }
    }

    /// <summary>
    /// Represents a result of some operation, with status information and an error message.
    /// </summary>
    /// <typeparam name="TValue">The result value type.</typeparam>
    public class Result<TValue> : Result
        where TValue : class
    {
        private readonly TValue? _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{TValueType}"/> class with the specified parameters.
        /// </summary>
        /// <param name="value">The result value.</param>
        /// <param name="isSuccess">The flag indicating if the result is successful.</param>
        /// <param name="error">The error message.</param>
        protected internal Result(TValue? value, bool isSuccess, string error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        /// <summary>
        /// Returns the result value if the result is successful, otherwise throws an exception.
        /// </summary>
        /// <returns>The result value if the result is successful.</returns>
        /// <exception cref="InvalidOperationException"> when IsSuccess is false.</exception>
        public TValue? Value()
        {
            if (!IsSuccess)
            {
                throw new InvalidOperationException();
            }

            return _value;
        }
    }
}
