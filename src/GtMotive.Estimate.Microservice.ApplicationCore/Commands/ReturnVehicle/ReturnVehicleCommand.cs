using System;
using FluentResults;
using GtMotive.Estimate.Microservice.Domain.Entities;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Commands.RentVehicle
{
    /// <summary>
    /// ReturnVehicleCommand.
    /// </summary>
    public class ReturnVehicleCommand : IRequest<Result<Vehicle>>
    {
        /// <summary>
        /// Gets or sets vehicle Id.
        /// </summary>
        public Guid VechicleId { get; set; }
    }
}
