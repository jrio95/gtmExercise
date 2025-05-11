using System;
using FluentResults;
using GtMotive.Estimate.Microservice.ApplicationCore.Dtos;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Features.CreateVehicle
{
    /// <summary>
    /// CreateVehicleCommand.
    /// </summary>
    public class CreateVehicleCommand : IRequest<Result<VehicleDto>>
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
