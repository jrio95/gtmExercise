using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using GtMotive.Estimate.Microservice.ApplicationCore.Repositories;
using GtMotive.Estimate.Microservice.ApplicationCore.ValidationServices;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Features.ReturnVehicle
{
    /// <summary>
    /// ReturnVehicleCommandHandler.
    /// </summary>
    public class ReturnVehicleCommandHandler : IRequestHandler<ReturnVehicleCommand, Result>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IClientRepository _clientRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleCommandHandler"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository.</param>
        /// <param name="clientRepository">The client repository.</param>
        public ReturnVehicleCommandHandler(IVehicleRepository vehicleRepository, IClientRepository clientRepository)
        {
            _vehicleRepository = vehicleRepository;
            _clientRepository = clientRepository;
        }

        /// <summary>Handles a request.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Response from the request.</returns>
        public async Task<Result> Handle(ReturnVehicleCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return Result.Fail("Return vehicle command is null");
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(request.VechicleId);

            var validationResult = ReturnleValidationService.Validate(vehicle);
            if (validationResult.IsFailed)
            {
                return validationResult;
            }

            var client = await _clientRepository.GetByIdAsync(vehicle.RentedById.Value);

            var rentResult = Result.Merge(vehicle.Return(), client.ReturnVehicle());
            if (rentResult.IsFailed)
            {
                return rentResult;
            }

            await _vehicleRepository.UpdateAsync(vehicle);
            await _clientRepository.UpdateAsync(client);

            return Result.Ok();
        }
    }
}
