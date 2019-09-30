﻿using System;

namespace ExpenseTracker.Domain.Exceptions
{
    /// <summary>
    /// Represents an error that occurs in the domain layer.
    /// </summary>
    public sealed class DomainException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public DomainException(string message)
            : base(message)
        {
        }
    }
}
