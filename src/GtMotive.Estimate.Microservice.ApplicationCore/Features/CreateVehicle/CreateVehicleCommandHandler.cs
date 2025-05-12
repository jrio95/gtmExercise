using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using GtMotive.Estimate.Microservice.ApplicationCore.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Features.CreateVehicle
{
    /// <summary>
    /// CreateVehicleCommandHandler.
    /// </summary>
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, Result<VehicleDto>>
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleCommandHandler"/> class.
        /// </summary>
        /// <param name="vehicleService">The vehicle service.</param>
        /// <param name="mapper">The mapper.</param>
        public CreateVehicleCommandHandler(IVehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
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

            var vehicleResult = await _vehicleService.CreateVehicleAsync(request.PlateNumber, request.ManufactureDate);
            return vehicleResult.IsSuccess
                ? _mapper.Map<VehicleDto>(vehicleResult.Value) :
                Result.Fail(vehicleResult.Errors);
        }
    }
}
