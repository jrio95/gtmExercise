using System;
using FluentResults;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Features.RentVehicle
{
    /// <summary>
    /// RentVehicleCommand.
    /// </summary>
    public class RentVehicleCommand : IRequest<Result>
    {
        /// <summary>
        /// Gets or sets vehicle Id.
        /// </summary>
        public Guid VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the identifier card number.
        /// </summary>
        /// <value>
        /// The identifier card number.
        /// </value>
        public string ClientIdCardNumber { get; set; }
    }
}
