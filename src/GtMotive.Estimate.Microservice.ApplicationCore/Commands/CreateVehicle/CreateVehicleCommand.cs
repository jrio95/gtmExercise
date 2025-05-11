using System;
using FluentResults;
using GtMotive.Estimate.Microservice.Domain.Entities;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Commands.CreateVehicle
{
    /// <summary>
    /// CreateVehicleCommand.
    /// </summary>
    public class CreateVehicleCommand : IRequest<Result<Vehicle>>
    {
        /// <summary>
        /// Gets or sets PlateNumber.
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// Gets or sets manufacture date.
        /// </summary>
        public DateTime ManufactureDate { get; set; }
    }
}
