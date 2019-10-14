using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Application.Commands.Exceptions
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
            IEnumerable<string> propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (string propertyName in propertyNames)
            {
                string[] propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }

        /// <summary>
        /// Gets the validation failures dictionary.
        /// </summary>
        public IDictionary<string, string[]> Failures { get; }
    }
}
