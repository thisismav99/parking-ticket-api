using Application.Applications.Vehicle.Command;
using Application.Applications.Vehicle.DTO;
using Application.Applications.Vehicle.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketingAPI.Utilities.Enums;
using ParkingTicketingAPI.Utilities.Helpers;

namespace ParkingTicketingAPI.Controllers.Vehicle
{
    public class VehicleController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public VehicleController(IMediator mediator, 
            LinkGenerator linkGenerator) : base(linkGenerator)
        {
            _mediator = mediator;
        }

        [HttpGet("{vehicleId:guid}")]
        public async Task<IActionResult> Get(Guid vehicleId, 
            CancellationToken cancellationToken)
        {
            var query = new GetVehicleByIdQuery(vehicleId);

            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int pageNumber, 
            int pageSize,
            CancellationToken cancellationToken)
        {
            var query = new GetVehiclesQuery(pageNumber, pageSize);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddVehicleDTO addVehicleDTO, 
            CancellationToken cancellationToken)
        {
            var command = new AddVehicleCommand(addVehicleDTO);

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtPostResponse(
             [
                LinkKeys.ById,
                LinkKeys.List,
                LinkKeys.Self
             ], result.Value);
        }
    }
}
