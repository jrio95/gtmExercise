using System;
using FluentResults;
using GtMotive.Estimate.Microservice.Domain.Entities;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Commands.RentVehicle
{
    /// <summary>
    /// RentVehicleCommand.
    /// </summary>
    public class RentVehicleCommand : IRequest<Result<Vehicle>>
    {
        /// <summary>
        /// Gets or sets vehicle Id.
        /// </summary>
        public Guid VechicleId { get; set; }

        /// <summary>
        /// Gets or sets the identifier card number.
        /// </summary>
        /// <value>
        /// The identifier card number.
        /// </value>
        public string ClientIdCardNumber { get; set; }
    }
}
