using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Domain.Primitives
{
    /// <summary>
    /// Contains methods for often performed validation checks.
    /// </summary>
    public static class Check
    {
        /// <summary>
        /// Checks if the specified is argument is not null, otherwise throws an <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <param name="arg">The argument to check.</param>
        /// <param name="argName">The name of the argument.</param>
        /// <exception cref="ArgumentNullException"> if the argument object is null.</exception>
        public static void NotNull(object arg, string argName)
        {
            if (arg == null)
            {
                throw new ArgumentNullException(argName);
            }
        }

        /// <summary>
        /// Checks if the specified collection is not null or empty, otherwise throws an <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <typeparam name="T">The argument type.</typeparam>
        /// <param name="arg">The argument to check.</param>
        /// <param name="argName">The name of the argument.</param>
        /// <exception cref="ArgumentNullException"> if the argument collection is null or empty.</exception>
        public static void NotNullOrEmpty<T>(IEnumerable<T> arg, string argName)
        {
            if (arg == null || !arg.Any())
            {
                throw new ArgumentNullException(argName);
            }
        }

        /// <summary>
        /// Checks if the specified is argument is not null or empty, otherwise throws an <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <param name="arg">The argument to check.</param>
        /// <param name="argName">The name of the argument.</param>
        /// <exception cref="ArgumentNullException"> if the argument string is null or empty.</exception>
        public static void NotNullOrEmpty(string arg, string argName)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                throw new ArgumentNullException(argName);
            }
        }

        /// <summary>
        /// Checks if the specified argument is greater than a lower bound, otherwise throws an <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="lowerBound">The lower bound for the check.</param>
        /// <param name="arg">The argument to check.</param>
        /// <param name="argName">The name of the argument.</param>
        /// <exception cref="ArgumentOutOfRangeException"> if the specified argument is not greater than the lower bound.</exception>
        public static void GreaterThan(int lowerBound, int arg, string argName)
        {
            if (arg <= lowerBound)
            {
                throw new ArgumentOutOfRangeException(argName);
            }
        }

        /// <summary>
        /// Checks if the specified start date-time precedes the specified end date-time, otherwise throws an <see cref="ArgumentOutOfRangeException"/>.
        /// </summary>
        /// <param name="startDate">The start date and time.</param>
        /// <param name="endDate">The end date and time.</param>
        /// <exception cref="ArgumentOutOfRangeException"> if the specified argument is not greater than the lower bound.</exception>
        public static void StartDatePrecedesEndDate(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                throw new ArgumentOutOfRangeException(nameof(startDate));
            }
        }

        /// <summary>
        /// Checks if the specified argument is greater than a lower bound, otherwise throws an <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="arg">The argument to check.</param>
        /// <param name="argName">The name of the argument.</param>
        /// <exception cref="ArgumentException"> if the specified argument is not greater than the lower bound.</exception>
        public static void NotEmpty(Guid arg, string argName)
        {
            if (arg == Guid.Empty)
            {
                throw new ArgumentException(argName);
            }
        }
    }
}
