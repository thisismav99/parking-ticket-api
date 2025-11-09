using Application.Vehicle.Command;
using Application.Vehicle.DTO;
using Application.Vehicle.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketingAPI.Utilities.Helpers;

namespace ParkingTicketingAPI.Controllers.Vehicle
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly LinkGenerator _linkGenerator;

        public VehicleController(IMediator mediator, 
            LinkGenerator linkGenerator)
        {
            _mediator = mediator;
            _linkGenerator = linkGenerator;
        }

        [HttpGet("{vehicleId:guid}")]
        public async Task<IActionResult> Get([FromRoute]Guid vehicleId, 
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
        public async Task<IActionResult> Get([FromRoute]int pageNumber, 
            [FromRoute]int pageSize,
            CancellationToken cancellationToken)
        {
            var query = new GetVehiclesQuery(pageNumber, pageSize);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddVehicleDTO addVehicleDTO, 
            CancellationToken cancellationToken)
        {
            var command = new AddVehicleCommand(addVehicleDTO.PlateNo,
                addVehicleDTO.Brand,
                addVehicleDTO.Color,
                addVehicleDTO.Model,
                addVehicleDTO.IsElectric,
                addVehicleDTO.IsHybrid,
                addVehicleDTO.CustomerId,
                addVehicleDTO.CreatedBy,
                addVehicleDTO.IsActive);

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            var linkGenerator = new Dictionary<string,string?>()
            {
                { "ById", _linkGenerator.GenerateLink(HttpContext, "Get", "Vehicle", result.Value) },
                { "List",  _linkGenerator.GenerateLink(HttpContext, "Get", "Vehicle", null) },
                { "Self", _linkGenerator.GenerateLink(HttpContext, "Post", "Vehicle", null) }
            };

            return Ok(linkGenerator);
        }
    }
}
