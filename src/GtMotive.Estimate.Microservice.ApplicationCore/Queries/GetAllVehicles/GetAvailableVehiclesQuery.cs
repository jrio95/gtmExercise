using System.Collections.Generic;
using GtMotive.Estimate.Microservice.Domain.Entities;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Queries.GetAllVehicles
{
    /// <summary>
    /// GetAvailableVehiclesQuery.
    /// </summary>
    public class GetAvailableVehiclesQuery : IRequest<IEnumerable<Vehicle>>
    {
    }
}
