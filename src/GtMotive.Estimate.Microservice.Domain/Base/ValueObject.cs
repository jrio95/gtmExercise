using System;

namespace GtMotive.Estimate.Microservice.Domain.Base
{
    /// <summary>
    /// ValueObject.
    /// </summary>
    /// <typeparam name="T">T param.</typeparam>
    public abstract class ValueObject<T>
        where T : IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValueObject{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        protected ValueObject(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public T Value { get; set; }
    }
}
