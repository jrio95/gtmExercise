using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces.Repositories;
using GtMotive.Estimate.Microservice.ApplicationCore.ValidationServices;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Features.RentVehicle
{
    /// <summary>
    /// RentVehicleCommandHandler.
    /// </summary>
    public class RentVehicleCommandHandler : IRequestHandler<RentVehicleCommand, Result>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IClientService _clientService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleCommandHandler"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository.</param>
        /// <param name="clientService">The client service.</param>
        public RentVehicleCommandHandler(IVehicleRepository vehicleRepository, IClientService clientService)
        {
            _vehicleRepository = vehicleRepository;
            _clientService = clientService;
        }

        /// <summary>Handles a request.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Response from the request.</returns>
        public async Task<Result> Handle(RentVehicleCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return Result.Fail("Rent vehicle command is null");
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(request.VechicleId);
            var clientResult = await _clientService.GetOrCreateClientAsync(request.ClientIdCardNumber);
            if (clientResult.IsFailed)
            {
                return Result.Fail(clientResult.Errors);
            }

            var client = clientResult.Value;
            RentVehicleValidationService.Validate(vehicle);
            var rentResult = Result.Merge(vehicle.Rent(client.Id), client.RentVehicle(vehicle.Id));

            if (rentResult.IsFailed)
            {
                return rentResult;
            }

            await _vehicleRepository.UpdateAsync(vehicle);
            await _clientService.UpdateClientAsync(client);

            return Result.Ok();
        }
    }
}
