using System.Collections.Generic;
using System.Text.RegularExpressions;
using ExpenseTracker.Domain.Primitives;

namespace ExpenseTracker.Domain.Aggregates.UserAggregate
{
    /// <summary>
    /// Represents an email.
    /// </summary>
    public sealed class Email : ValueObject
    {
        private const string EmailRegexPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|
([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        /// <summary>
        /// Initializes a new instance of the <see cref="Email"/> class with the specified value.
        /// </summary>
        /// <param name="email">The email value.</param>
        private Email(string email)
        {
            Value = email;
        }
        
        /// <summary>
        /// Gets the email value.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Creates a new <see cref="Email"/> object with the specified email.
        /// </summary>
        /// <param name="email">The email value.</param>
        /// <returns>A new <see cref="Email"/> instance, or an error result.</returns>
        public static Result<Email> Create(string email)
        {
            email = (email ?? string.Empty).Trim();

            if (email.Length == 0)
            {
                return Result.Fail<Email>("Email should not be empty.");
            }

            bool isRegexMatch = Regex.IsMatch(email, EmailRegexPattern, RegexOptions.IgnoreCase);

            return isRegexMatch ? Result.Ok(new Email(email)) : Result.Fail<Email>("Email is invalid.");
        }

        public static implicit operator string(Email email) => email.Value;

        public static explicit operator Email(string email) => Create(email).Value;

        /// <inheritdoc />
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
