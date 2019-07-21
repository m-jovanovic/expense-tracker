using System;

namespace ExpenseTracker.Application.Abstractions
{
    /// <summary>
    /// Represents the date time interface.
    /// </summary>
    public interface IDateTime
    {
        /// <summary>
        /// Gets the current UTC time.
        /// </summary>
        /// <returns>The current date and time expressed as UTC.</returns>
        DateTime UtcNow();
    }
}
