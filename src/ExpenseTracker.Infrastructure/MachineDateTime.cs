using System;
using ExpenseTracker.Application.Abstractions;

namespace ExpenseTracker.Infrastructure
{
    /// <summary>
    /// Represents the date time on the current machine.
    /// </summary>
    public class MachineDateTime : IDateTime
    {
        /// <inheritdoc />
        public DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
