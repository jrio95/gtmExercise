using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using GtMotive.Estimate.Microservice.ApplicationCore.Repositories;
using GtMotive.Estimate.Microservice.ApplicationCore.ValidationServices;
using GtMotive.Estimate.Microservice.Domain.Entities;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Commands.RentVehicle
{
    /// <summary>
    /// RentVehicleCommandHandler.
    /// </summary>
    public class RentVehicleCommandHandler : IRequestHandler<RentVehicleCommand, Result<Vehicle>>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IClientRepository _clientRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleCommandHandler"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository.</param>
        /// <param name="clientRepository">The client repository.</param>
        public RentVehicleCommandHandler(IVehicleRepository vehicleRepository, IClientRepository clientRepository)
        {
            _vehicleRepository = vehicleRepository;
            _clientRepository = clientRepository;
        }

        /// <summary>Handles a request.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Response from the request.</returns>
        public async Task<Result<Vehicle>> Handle(RentVehicleCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return Result.Fail("Rent vehicle command is null");
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(request.VechicleId);
            var client = await _clientRepository.GetByCardNumberAsync(request.ClientIdCardNumber);

            if (client == null)
            {
                var createdClientResult = await CreateNewClient(request.ClientIdCardNumber);
                if (createdClientResult.IsFailed)
                {
                    return Result.Fail("Error while creating the client");
                }

                client = createdClientResult.Value;
            }

            RentVehicleValidationService.Validate(vehicle);
            var rentResult = Result.Merge(vehicle.Rent(client.Id), client.RentVehicle(vehicle.Id));

            if (rentResult.IsFailed)
            {
                return rentResult;
            }

            await _vehicleRepository.UpdateAsync(vehicle);
            await _clientRepository.UpdateAsync(client);

            return vehicle;
        }

        private async Task<Result<Client>> CreateNewClient(string clientIdCardNumber)
        {
            var clientToCreateResult = Client.Create(clientIdCardNumber);
            if (clientToCreateResult.IsFailed)
            {
                return clientToCreateResult;
            }

            var client = clientToCreateResult.Value;
            await _clientRepository.AddAsync(client);

            return client;
        }
    }
}
