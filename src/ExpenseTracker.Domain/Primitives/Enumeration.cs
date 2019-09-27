using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExpenseTracker.Domain.Exceptions;

namespace ExpenseTracker.Domain.Primitives
{
    /// <summary>
    /// Represents an enumeration type.
    /// </summary>
    public abstract class Enumeration : IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Enumeration"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="name">The name.</param>
        protected Enumeration(int value, string name)
        {
            Value = value;
            Name = name;
        }

        protected Enumeration()
        {
            Value = default;
            Name = string.Empty;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Creates an enumeration of the specified type based on the specified value.
        /// </summary>
        /// <typeparam name="T">The enumeration type.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The enumeration instance that matches the specified value.</returns>
        /// <exception cref="DomainException"> if the specified value doesn't match any value in the enumeration.</exception>
        public static Maybe<T> FromValue<T>(int value)
            where T : Enumeration
        {
            T matchingItem = GetAll<T>()
                .FirstOrDefault(item => item.Value == value);

            return matchingItem;
        }

        /// <summary>
        /// Gets all of the enumeration values for the specified type.
        /// </summary>
        /// <typeparam name="T">The enumeration type.</typeparam>
        /// <returns>The enumerable collection of values for the specified type.</returns>
        public static IEnumerable<T> GetAll<T>()
            where T : Enumeration
        {
            Type type = typeof(T);

            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (FieldInfo info in fields)
            {
                object? o = Activator.CreateInstance(typeof(T), true);

                if (o is null)
                {
                    throw new Exception("Test");
                }

                var instance = (T)o;

                if (info.GetValue(instance) is T locatedValue)
                {
                    yield return locatedValue;
                }
            }
        }

        /// <inheritdoc />
        public int CompareTo(object? other)
        {
            return other is null ? 1 : Value.CompareTo(((Enumeration)other).Value);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if (!(obj is Enumeration otherValue))
            {
                return false;
            }

            return GetType() == obj.GetType() && otherValue.Value.Equals(Value);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }
    }
}
