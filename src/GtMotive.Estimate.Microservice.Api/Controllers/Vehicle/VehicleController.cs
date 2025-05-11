using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.Api.Requests;
using GtMotive.Estimate.Microservice.ApplicationCore.Commands.CreateVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.Commands.RentVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.Queries.GetAllVehicles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers.Vehicle
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public VehicleController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleRequest request)
        {
            var command = _mapper.Map<CreateVehicleCommand>(request);

            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok() : BadRequest(result.Errors.Select(e => e.Message));
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableVehicles()
        {
            var result = await _mediator.Send(new GetAvailableVehiclesQuery());

            return Ok(result);
        }

        [HttpPost("{id}/rent")]
        public async Task<IActionResult> Rent([FromRoute] Guid id, [FromBody] RentVehicleRequest request)
        {
            var command = _mapper.Map<RentVehicleCommand>(request);
            command.VechicleId = id;

            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? Ok()
                : BadRequest(new { Errors = result.Errors.Select(e => e.Message) });
        }

        [HttpPost("{id}/return")]
        public async Task<IActionResult> Return([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new ReturnVehicleCommand()
            {
                VechicleId = id
            });

            return result.IsSuccess
                ? Ok()
                : BadRequest(new { Errors = result.Errors.Select(e => e.Message) });
        }
    }
}
