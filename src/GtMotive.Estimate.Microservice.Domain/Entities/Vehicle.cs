using System;
using FluentResults;
using GtMotive.Estimate.Microservice.Domain.Base;
using GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects;
using MongoDB.Bson.Serialization.Attributes;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Vehicle entity.
    /// </summary>
    public class Vehicle : EntityBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        /// <param name="plateNumber">The plate number.</param>
        /// <param name="manufacturedDate">The manufactured date.</param>
        private Vehicle(PlateNumber plateNumber, DateTime manufacturedDate)
            : base()
        {
            PlateNumber = plateNumber;
            IsRented = false;
            ManufactureDate = manufacturedDate;
        }

        /// <summary>
        /// Gets PlateNumber.
        /// </summary>
        public PlateNumber PlateNumber { get; private set; }

        /// <summary>
        /// Gets a value indicating whether gets IsRented.
        /// </summary>
        public bool IsRented { get; private set; }

        /// <summary>
        /// Gets a value the id of the client who rented the vehicle if exists.
        /// </summary>
        public Guid? RentedById { get; private set; }

        /// <summary>
        /// Gets manufacture date.
        /// </summary>
        public DateTime ManufactureDate { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is available.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this vehicle is available; otherwise, <c>false</c>.
        /// </value>
        [BsonIgnore]
        public bool IsAvailable =>
            !IsRented && ManufactureDate > DateTime.UtcNow.AddYears(-5);

        /// <summary>
        /// Creates a vehicle instance.
        /// </summary>
        /// <param name="plate">The plate.</param>
        /// <param name="manufacturedDate">The manufactured date.</param>
        /// <returns>Result of the created instance.</returns>
        public static Result<Vehicle> Create(string plate, DateTime manufacturedDate)
        {
            var plateNumberResult = PlateNumber.Create(plate);
            if (plateNumberResult.IsFailed)
            {
                return Result.Fail(plateNumberResult.Errors);
            }
            else if (manufacturedDate == default)
            {
                return Result.Fail("Manufacture date is required.");
            }

            return Result.Ok(new Vehicle(plateNumberResult.Value, manufacturedDate));
        }

        /// <summary>
        /// Rents this instance.
        /// </summary>
        /// <param name="clientIdToRent">The client identifier to rent.</param>
        /// <returns>Result.</returns>
        public Result Rent(Guid clientIdToRent)
        {
            if (!IsAvailable)
            {
                return Result.Fail("This vehicle can not be rented");
            }

            if (RentedById != null)
            {
                return Result.Fail("This vehicle is already rented");
            }

            if (clientIdToRent == Guid.Empty)
            {
                return Result.Fail("Client does not exist");
            }

            IsRented = true;
            RentedById = clientIdToRent;

            return Result.Ok();
        }

        /// <summary>
        /// Returns this instance.
        /// </summary>
        /// <returns>Result.</returns>
        public Result Return()
        {
            if (RentedById == null)
            {
                return Result.Fail("This vehicle is already rented");
            }

            IsRented = false;
            RentedById = null;

            return Result.Ok();
        }
    }
}
