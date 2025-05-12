using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Features.RentVehicle
{
    /// <summary>
    /// RentVehicleCommandHandler.
    /// </summary>
    public class RentVehicleCommandHandler : IRequestHandler<RentVehicleCommand, Result>
    {
        private readonly IVehicleService _vehicleService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleCommandHandler"/> class.
        /// </summary>
        /// <param name="vehicleService">The vehicle service.</param>
        public RentVehicleCommandHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        /// <summary>Handles a request.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Response from the request.</returns>
        public async Task<Result> Handle(RentVehicleCommand request, CancellationToken cancellationToken)
        {
            return request == null
                ? Result.Fail("Rent vehicle command is null")
                : await _vehicleService.RentVehicleAsync(request.VehicleId, request.ClientIdCardNumber);
        }
    }
}
