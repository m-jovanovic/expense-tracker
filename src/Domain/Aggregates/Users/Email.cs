using System.Collections.Generic;
using System.Text.RegularExpressions;
using Domain.Core.Extensions;
using Domain.Core.Primitives;

namespace Domain.Aggregates.Users
{
    /// <summary>
    /// Represents an email.
    /// </summary>
    public sealed class Email : ValueObject
    {
        /// <summary>
        /// Returns an empty email object.
        /// </summary>
        internal static readonly Email Empty = new Email();

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
            Value = string.Empty;
        }

        /// <summary>
        /// Gets the email value.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Creates a new <see cref="Email"/> instance with the specified email.
        /// </summary>
        /// <param name="email">The email value.</param>
        /// <returns>A new <see cref="Email"/> instance if the email is valid, or an error result.</returns>
        public static Result<Email> Create(string? email)
        {
            return email
                .ToResult("Email should not be empty.")
                .OnSuccess(e => e?.Trim())
                .Ensure(e => e?.Length != 0, "Email should not be empty")
                .Ensure(e => e?.Length < 256, "Email is too long.")
                .Ensure(e => Regex.IsMatch(e, EmailRegexPattern, RegexOptions.IgnoreCase), "Email is invalid.")
                .Map(e => e is null ? Empty : new Email(e));
        }

        public static implicit operator string(Email email) => email?.Value ?? string.Empty;

        public static explicit operator Email(string email)
        {
            Result<Email> emailResult = Create(email);

            Email? emailValue = emailResult.Value();

            return emailResult.IsFailure || emailValue is null ? Empty : emailValue;
        }

        /// <inheritdoc />
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
