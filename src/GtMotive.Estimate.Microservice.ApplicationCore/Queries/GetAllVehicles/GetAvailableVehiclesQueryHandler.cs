using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Repositories;
using GtMotive.Estimate.Microservice.Domain.Entities;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Queries.GetAllVehicles
{
    /// <summary>
    /// GetAvailableVehiclesQueryHandler.
    /// </summary>
    public class GetAvailableVehiclesQueryHandler : IRequestHandler<GetAvailableVehiclesQuery, IEnumerable<Vehicle>>
    {
        private readonly IVehicleRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAvailableVehiclesQueryHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public GetAvailableVehiclesQueryHandler(IVehicleRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Handles the specified query.
        /// </summary>
        /// <param name="request">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>List of available vehicles.</returns>
        public async Task<IEnumerable<Vehicle>> Handle(GetAvailableVehiclesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAvailableAsync();
        }
    }
}
