using System;
using FluentResults;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Features.ReturnVehicle
{
    /// <summary>
    /// ReturnVehicleCommand.
    /// </summary>
    public class ReturnVehicleCommand : IRequest<Result>
    {
        /// <summary>
        /// Gets or sets vehicle Id.
        /// </summary>
        public Guid VechicleId { get; set; }
    }
}
