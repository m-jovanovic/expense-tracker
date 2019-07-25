using System.Collections.Generic;
using System.Text.RegularExpressions;
using ExpenseTracker.Domain.Extensions;
using ExpenseTracker.Domain.Primitives;

namespace ExpenseTracker.Domain.Aggregates.UserAggregate
{
    /// <summary>
    /// Represents an email.
    /// </summary>
    public sealed class Email : ValueObject
    {
        private const string EmailRegexPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        /// <summary>
        /// Initializes a new instance of the <see cref="Email"/> class with the specified value.
        /// </summary>
        /// <param name="email">The email value.</param>
        private Email(string email)
        {
            Value = email;
        }

        private Email()
        {
        }

        /// <summary>
        /// Gets the email value.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Creates a new <see cref="Email"/> object with the specified email.
        /// </summary>
        /// <param name="emailOrNothing">The email value or nothing.</param>
        /// <returns>A new <see cref="Email"/> instance, or an error result.</returns>
        public static Result<Email> Create(Maybe<string> emailOrNothing)
        {
            return emailOrNothing
                .ToResult("Email should not be empty.")
                .OnSuccess(email => email.Trim())
                .Ensure(email => email.Length != 0, "Email should not be empty")
                .Ensure(email => email.Length < 256, "Email is too long.")
                .Ensure(email => Regex.IsMatch(email, EmailRegexPattern, RegexOptions.IgnoreCase), "Email is invalid.")
                .Map(email => new Email(email));
        }

        public static implicit operator string(Email email) => email?.Value ?? string.Empty;

        public static explicit operator Email(string email) => Create(email).Value;

        /// <inheritdoc />
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
