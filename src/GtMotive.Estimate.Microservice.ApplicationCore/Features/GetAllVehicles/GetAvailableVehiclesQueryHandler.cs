using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Repositories;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Features.GetAllVehicles
{
    /// <summary>
    /// GetAvailableVehiclesQueryHandler.
    /// </summary>
    public class GetAvailableVehiclesQueryHandler : IRequestHandler<GetAvailableVehiclesQuery, IEnumerable<VehicleDto>>
    {
        private readonly IVehicleRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAvailableVehiclesQueryHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        public GetAvailableVehiclesQueryHandler(IVehicleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the specified query.
        /// </summary>
        /// <param name="request">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>List of available vehicles.</returns>
        public async Task<IEnumerable<VehicleDto>> Handle(GetAvailableVehiclesQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<VehicleDto>>(await _repository.GetAvailableAsync());
        }
    }
}
