using System;

namespace ExpenseTracker.Domain.Primitives
{
    /// <summary>
    /// Represents a wrapper around a value, which may be empty.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public struct Maybe<T> : IEquatable<Maybe<T>>
        where T : class
    {
        private readonly T _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Maybe{T}"/> struct with the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        private Maybe(T value)
        {
            _value = value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public T Value
        {
            get
            {
                if (HasNoValue)
                {
                    throw new InvalidOperationException();
                }

                return _value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the object contains a value.
        /// </summary>
        public bool HasValue => _value != null;

        /// <summary>
        /// Gets a value indicating whether the object does not contain a value.
        /// </summary>
        public bool HasNoValue => !HasValue;

        public static bool operator ==(Maybe<T> maybe, T value)
        {
            return maybe.HasValue && maybe.Value.Equals(value);
        }

        public static bool operator !=(Maybe<T> maybe, T value)
        {
            return !(maybe == value);
        }

        public static bool operator ==(Maybe<T> first, Maybe<T> second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Maybe<T> first, Maybe<T> second)
        {
            return !(first == second);
        }

        public static implicit operator Maybe<T>(T value) => new Maybe<T>(value);

        /// <inheritdoc />
        public bool Equals(Maybe<T> other)
        {
            if (HasNoValue && other.HasNoValue)
            {
                return true;
            }

            if (HasNoValue || other.HasNoValue)
            {
                return false;
            }

            return _value.Equals(other._value);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (!(obj is Maybe<T>))
            {
                return false;
            }

            var other = (Maybe<T>)obj;

            return Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        /// <summary>
        /// Unwraps the <see cref="Maybe{T}"/> object, returning the contained value or the default value for its type.
        /// </summary>
        /// <returns>The containing value, or the default value for its type.</returns>
        public T? Unwrap()
        {
            return HasValue ? Value : default;
        }

        /// <summary>
        /// Unwraps the <see cref="Maybe{T}"/> object, returning the result of the specified selector function or the default value for its type.
        /// </summary>
        /// <typeparam name="TProperty">The property selector for the value.</typeparam>
        /// <param name="selector">The selector function.</param>
        /// <returns>The result of the specified selector function or the default value for its type.</returns>
        public TProperty Unwrap<TProperty>(Func<T, TProperty> selector)
        {
            Check.NotNull(selector, nameof(selector));

            return HasValue ? selector(Value) : default;
        }
    }
}
