using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using GtMotive.Estimate.Microservice.ApplicationCore.Repositories;
using GtMotive.Estimate.Microservice.Domain.Entities;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Commands.CreateVehicle
{
    /// <summary>
    /// CreateVehicleCommandHandler.
    /// </summary>
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, Result<Vehicle>>
    {
        private readonly IVehicleRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleCommandHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public CreateVehicleCommandHandler(IVehicleRepository repository)
        {
            _repository = repository;
        }

        /// <summary>Handles a request.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Response from the request.</returns>
        public async Task<Result<Vehicle>> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return Result.Fail("Request can not be null");
            }

            var vehicleResult = Vehicle.Create(request.PlateNumber, request.ManufactureDate);
            if (vehicleResult.IsFailed)
            {
                return vehicleResult;
            }

            await _repository.AddAsync(vehicleResult.Value);

            return vehicleResult.Value;
        }
    }
}
