using System.Collections.Generic;
using GtMotive.Estimate.Microservice.ApplicationCore.Dtos;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Features.GetAllVehicles
{
    /// <summary>
    /// GetAvailableVehiclesQuery.
    /// </summary>
    public class GetAvailableVehiclesQuery : IRequest<IEnumerable<VehicleDto>>
    {
    }
}
