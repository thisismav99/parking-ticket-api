using Application.Applications.Parking.Command;
using Application.Applications.Parking.DTO;
using Application.Applications.Parking.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketingAPI.Utilities.Enums;

namespace ParkingTicketingAPI.Controllers.Parking
{
    public class ParkingController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public ParkingController(IMediator mediator,
            LinkGenerator linkGenerator) : base(linkGenerator)  
        {
            _mediator = mediator;
        }

        [HttpGet("{parkingId:guid}")]
        public async Task<IActionResult> Get(Guid parkingId, CancellationToken cancellationToken)
        {
            var query = new GetParkingByIdQuery(parkingId);

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
            var query = new GetParkingsQuery(pageNumber, pageSize);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddParkingDTO addParkingDTO,
            CancellationToken cancellationToken)
        {
            var command = new AddParkingCommand(addParkingDTO);

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
