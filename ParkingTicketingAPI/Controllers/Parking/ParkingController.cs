using Application.Applications.Parking.Command;
using Application.Applications.Parking.DTO;
using Application.Applications.Parking.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketingAPI.Utilities.Helpers;

namespace ParkingTicketingAPI.Controllers.Parking
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly LinkGenerator _linkGenerator;

        public ParkingController(IMediator mediator,
            LinkGenerator linkGenerator)
        {
            _mediator = mediator;
            _linkGenerator = linkGenerator;
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
            var command = new AddParkingCommand(addParkingDTO.ParkDateTime,
                addParkingDTO.ExitDateTime,
                addParkingDTO.VehicleId,
                addParkingDTO.EmployeeId,
                addParkingDTO.TransactionId,
                addParkingDTO.CreatedBy,
                addParkingDTO.IsActive);

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            var linkGenerator = new Dictionary<string, string?>()
            {
                { "ById", _linkGenerator.GenerateLink(HttpContext, "Get", "Parking", result.Value) },
                { "List",  _linkGenerator.GenerateLink(HttpContext, "Get", "Parking", null) },
                { "Self", _linkGenerator.GenerateLink(HttpContext, "Post", "Parking", null) }
            };

            return Ok(linkGenerator);
        }
    }
}
