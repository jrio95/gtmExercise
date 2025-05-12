using System;
using FluentResults;
using GtMotive.Estimate.Microservice.Domain.Base;
using GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects;
using MongoDB.Bson.Serialization.Attributes;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Client entity.
    /// </summary>
    public class Client : EntityBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="cardNumber">card number.</param>
        private Client(CardNumber cardNumber)
            : base()
        {
            CardNumber = cardNumber;
        }

        /// <summary>
        /// Gets CardNumber.
        /// </summary>
        public CardNumber CardNumber { get; private set; }

        /// <summary>
        /// Gets the rented vehicle identifier.
        /// </summary>
        /// <value>
        /// The rented vehicle identifier.
        /// </value>
        public Guid? RentedVehicleId { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance can rent.
        /// </summary>
        /// <value>
        /// Bool indicating if the client can rent a car.
        /// </value>
        [BsonIgnore]
        public bool CanRent => RentedVehicleId == null;

        /// <summary>
        /// Create method.
        /// </summary>
        /// <param name="cardNumber">cardNumber.</param>
        /// <returns>Entity of the Client encapsulated in result.</returns>
        public static Result<Client> Create(string cardNumber)
        {
            var cardNumberResult = CardNumber.Create(cardNumber);
            return cardNumberResult.IsFailed
                ? (Result<Client>)Result.Fail(cardNumberResult.Errors) : Result.Ok(new Client(cardNumberResult.Value));
        }

        /// <summary>
        /// Rents the vehicle.
        /// </summary>
        /// <param name="vehicleIdToRent">The vehicle identifier to rent.</param>
        /// <returns>Result.</returns>
        public Result RentVehicle(Guid vehicleIdToRent)
        {
            if (!CanRent)
            {
                return Result.Fail("Client can not rent another car");
            }

            RentedVehicleId = vehicleIdToRent;

            return Result.Ok();
        }

        /// <summary>
        /// Returns the vehicle.
        /// </summary>
        /// <returns>Result.</returns>
        public Result ReturnVehicle()
        {
            if (CanRent)
            {
                return Result.Fail("Client has no vehicles rented");
            }

            RentedVehicleId = null;

            return Result.Ok();
        }
    }
}
