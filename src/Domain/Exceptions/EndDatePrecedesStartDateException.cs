using System;

namespace Domain.Exceptions
{
    /// <summary>
    /// Represents the exception when and date for a date range precedes the start date.
    /// </summary>
    public class EndDatePrecedesStartDateException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EndDatePrecedesStartDateException"/> class.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        public EndDatePrecedesStartDateException(DateTime startDate, DateTime endDate)
            : base($"The end date {endDate:yyyy-MM-dd} can not precede the start date {startDate:yyyy-MM-dd}.")
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        /// <summary>
        /// Gets the start date.
        /// </summary>
        public DateTime StartDate { get; }

        /// <summary>
        /// Gets the end date.
        /// </summary>
        public DateTime EndDate { get; }
    }
}