using System;

namespace ExpenseTracker.Application.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs when an entity is not found.
    /// </summary>
    public sealed class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Entity ${name} with key {key} was not found.")
        {
        }
    }
}
