using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentResults;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Interfaces
{
    /// <summary>
    /// IVehicleService.
    /// </summary>
    public interface IVehicleService
    {
        /// <summary>
        /// Creates the vehicle asynchronous.
        /// </summary>
        /// <param name="plateNumber">The plate number.</param>
        /// <param name="manufacturedDate">The manufactured date.</param>
        /// <returns>Created vehicle.</returns>
        Task<Result<Vehicle>> CreateVehicleAsync(string plateNumber, DateTime manufacturedDate);

        /// <summary>
        /// Gets the available asynchronous.
        /// </summary>
        /// <returns>Colletion of available vehicles.</returns>
        Task<IEnumerable<Vehicle>> GetAvailableAsync();

        /// <summary>
        /// Rents the vehicle asynchronous.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <param name="clientCardNumber">The client card number.</param>
        /// <returns>Result.</returns>
        Task<Result> RentVehicleAsync(Guid vehicleId, string clientCardNumber);

        /// <summary>
        /// Returns the vehicle asynchronous.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns>Result.</returns>
        Task<Result> ReturnVehicleAsync(Guid vehicleId);
    }
}
