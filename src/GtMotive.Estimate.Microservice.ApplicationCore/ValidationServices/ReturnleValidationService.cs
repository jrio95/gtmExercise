using FluentResults;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.ApplicationCore.ValidationServices
{
    /// <summary>
    /// ReturnleValidationService.
    /// </summary>
    public static class ReturnleValidationService
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
            if (vehicle == null)
            {
                return Result.Fail("Vehicle not found");
            }
            else if (vehicle.RentedById == null)
            {
                return Result.Fail("You can not return a vehicle which is not rented");
            }

            return Result.Ok();
        }
    }
}
