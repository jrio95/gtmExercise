using FluentResults;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.ApplicationCore.ValidationServices
{
    /// <summary>
    /// RentVehicleValidationService.
    /// </summary>
    public static class RentVehicleValidationService
    {
        /// <summary>
        /// Validates if the specified vehicle can be rented.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        /// <returns>
        ///   Result.
        /// </returns>
        public static Result Validate(Vehicle vehicle)
        {
            return vehicle == null ? Result.Fail("Vehicle does not exist") : Result.Ok();
        }
    }
}
