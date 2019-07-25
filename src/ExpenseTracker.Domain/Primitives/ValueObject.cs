using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Domain.Primitives
{
    /// <summary>
    /// Represents the base class all value objects derive from.
    /// </summary>
    public abstract class ValueObject
    {
        public static bool operator ==(ValueObject a, ValueObject b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            var valueObject = (ValueObject)obj;

            return GetAtomicValues().SequenceEqual(valueObject.GetAtomicValues());
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            HashCode hashCode = default;

            // Since a value object may contain more than 8 properties we can't use HashCode.Combine.
            foreach (object obj in GetAtomicValues())
            {
                hashCode.Add(obj);
            }

            return hashCode.ToHashCode();
        }

        /// <summary>
        /// Gets the atomic values of the value object.
        /// </summary>
        /// <returns>The collection of objects representing the value object values.</returns>
        protected abstract IEnumerable<object> GetAtomicValues();
    }
}