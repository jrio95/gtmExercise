using FluentResults;
using GtMotive.Estimate.Microservice.Domain.Base;

namespace GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects
{
    /// <summary>
    /// PlateNumber.
    /// </summary>
    public class CardNumber : ValueObject<string>
    {
        private CardNumber(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Creates the specified plate.
        /// </summary>
        /// <param name="plate">The plate.</param>
        /// <returns>PlateNumber.</returns>
        public static Result<CardNumber> Create(string plate)
        {
            if (string.IsNullOrWhiteSpace(plate))
            {
                return Result.Fail("Card number is required");
            }
            else if (plate.Length > 100)
            {
                return Result.Fail("Card number is too long");
            }

            return Result.Ok(new CardNumber(plate));
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A string that represents this instance.
        /// </returns>
        public override string ToString() => Value;
    }
}
