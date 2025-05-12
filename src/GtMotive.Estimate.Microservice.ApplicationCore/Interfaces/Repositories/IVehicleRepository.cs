using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Interfaces.Repositories
{
    /// <summary>
    /// IVehicleRepository.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Add a vehicle.
        /// </summary>
        /// <param name="vehicle">Vehicle to be added.</param>
        /// <returns>Task.</returns>
        Task AddAsync(Vehicle vehicle);

        /// <summary>Gets all available.</summary>
        /// <returns>
        /// List of available vehicles.
        /// </returns>
        Task<IEnumerable<Vehicle>> GetAvailableAsync();

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Vehicle.</returns>
        Task<Vehicle> GetByIdAsync(Guid id);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        /// <returns>Task.</returns>
        Task UpdateAsync(Vehicle vehicle);
    }
}
