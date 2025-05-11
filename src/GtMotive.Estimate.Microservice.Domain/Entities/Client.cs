using System;
using FluentResults;

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
        /// <param name="plateNumber">plate number.</param>
        private Client(string idCardNumber)
            : base()
        {
            IdCardNumber = idCardNumber;
        }

        /// <summary>
        /// Gets PlateNumber.
        /// </summary>
        public string IdCardNumber { get; private set; }

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
        public bool CanRent => RentedVehicleId == null;

        /// <summary>
        /// Create method.
        /// </summary>
        /// <param name="idCardNumber">idCardNumber.</param>
        /// <returns>Entity of the Client encapsulated in result.</returns>
        public static Result<Client> Create(string idCardNumber)
        {
            return string.IsNullOrEmpty(idCardNumber)
                ? (Result<Client>)Result.Fail("No id card found for the client") : Result.Ok(new Client(idCardNumber));
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
