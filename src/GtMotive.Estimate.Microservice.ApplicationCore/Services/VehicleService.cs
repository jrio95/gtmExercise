using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentResults;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces.Repositories;
using GtMotive.Estimate.Microservice.ApplicationCore.ValidationServices;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Services
{
    /// <summary>
    /// VehicleService.
    /// </summary>
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IClientService _clientService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleService"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository.</param>
        /// <param name="clientRepository">The client repository.</param>
        /// <param name="clientService">The client service.</param>
        public VehicleService(
            IVehicleRepository vehicleRepository,
            IClientRepository clientRepository,
            IClientService clientService)
        {
            _vehicleRepository = vehicleRepository;
            _clientRepository = clientRepository;
            _clientService = clientService;
        }

        /// <summary>
        /// Creates the vehicle asynchronous.
        /// </summary>
        /// <param name="plateNumber">The plate number.</param>
        /// <param name="manufacturedDate">The manufactured date.</param>
        /// <returns>Vehicle Result.</returns>
        public async Task<Result<Vehicle>> CreateVehicleAsync(string plateNumber, DateTime manufacturedDate)
        {
            var vehicleResult = Vehicle.Create(plateNumber, manufacturedDate);
            if (vehicleResult.IsFailed)
            {
                return Result.Fail(vehicleResult.Errors);
            }

            await _vehicleRepository.AddAsync(vehicleResult.Value);

            return vehicleResult.Value;
        }

        /// <summary>
        /// Gets the available asynchronous.
        /// </summary>
        /// <returns>Collection of vehicles.</returns>
        public async Task<IEnumerable<Vehicle>> GetAvailableAsync()
        {
            return await _vehicleRepository.GetAvailableAsync();
        }

        /// <summary>
        /// Rents the vehicle asynchronous.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <param name="clientCardNumber">The client card number.</param>
        /// <returns>Result.</returns>
        public async Task<Result> RentVehicleAsync(Guid vehicleId, string clientCardNumber)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);
            var clientResult = await _clientService.GetOrCreateClientAsync(clientCardNumber);
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
            await _clientRepository.UpdateAsync(client);

            return Result.Ok();
        }

        /// <summary>
        /// Returns the vehicle asynchronous.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns>Result.</returns>
        public async Task<Result> ReturnVehicleAsync(Guid vehicleId)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);

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
