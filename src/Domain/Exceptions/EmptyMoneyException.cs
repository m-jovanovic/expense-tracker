using Domain.Core.Exceptions;

namespace Domain.Exceptions
{
    /// <summary>
    /// Represents the exception for an empty money instance.
    /// </summary>
    public sealed class EmptyMoneyException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyMoneyException"/> class.
        /// </summary>
        public EmptyMoneyException()
            : base("The specified money instance is empty.")
        {
        }
    }
}
