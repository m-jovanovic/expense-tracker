using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Domain.Core.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs when a validation fails.
    /// </summary>
    public sealed class ValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Failures = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="failures">The collection of validation failures.</param>
        public ValidationException(IReadOnlyCollection<ValidationFailure> failures)
            : this()
        {
            Failures = failures
                .GroupBy(f => f.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(f => f.ErrorMessage).ToArray());
        }

        /// <summary>
        /// Gets the validation failures dictionary.
        /// </summary>
        public IDictionary<string, string[]> Failures { get; }
    }
}
