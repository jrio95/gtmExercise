using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Features.ReturnVehicle
{
    /// <summary>
    /// ReturnVehicleCommandHandler.
    /// </summary>
    public class ReturnVehicleCommandHandler : IRequestHandler<ReturnVehicleCommand, Result>
    {
        private readonly IVehicleService _vehicleService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleCommandHandler"/> class.
        /// </summary>
        /// <param name="vehicleService">The vehicle service.</param>
        public ReturnVehicleCommandHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        /// <summary>Handles a request.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Response from the request.</returns>
        public async Task<Result> Handle(ReturnVehicleCommand request, CancellationToken cancellationToken)
        {
            return request == null ?
                Result.Fail("Return vehicle command is null") :
                await _vehicleService.ReturnVehicleAsync(request.VehicleId);
        }
    }
}
