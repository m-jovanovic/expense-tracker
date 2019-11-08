using System;
using Application.Abstractions;

namespace Infrastructure
{
    /// <summary>
    /// Represents the date time on the current machine.
    /// </summary>
    internal sealed class MachineDateTime : IDateTime
    {
        /// <inheritdoc />
        public DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
