using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using GtMotive.Estimate.Microservice.ApplicationCore.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces.Repositories;
using GtMotive.Estimate.Microservice.Domain.Entities;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Features.CreateVehicle
{
    /// <summary>
    /// CreateVehicleCommandHandler.
    /// </summary>
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, Result<VehicleDto>>
    {
        private readonly IVehicleRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleCommandHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        public CreateVehicleCommandHandler(IVehicleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>Handles a request.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Response from the request.</returns>
        public async Task<Result<VehicleDto>> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return Result.Fail("Request can not be null");
            }

            var vehicleResult = Vehicle.Create(request.PlateNumber, request.ManufactureDate);
            if (vehicleResult.IsFailed)
            {
                return Result.Fail<VehicleDto>(vehicleResult.Errors);
            }

            await _repository.AddAsync(vehicleResult.Value);

            return _mapper.Map<VehicleDto>(vehicleResult.Value);
        }
    }
}
