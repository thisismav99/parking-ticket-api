using Application.Applications.Common.Command;
using Application.Applications.Common.DTO;
using Application.Applications.Common.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketingAPI.Utilities.Helpers;

namespace ParkingTicketingAPI.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly LinkGenerator _linkGenerator;

        public AddressController(IMediator mediator,
            LinkGenerator linkGenerator)
        {
            _mediator = mediator;
            _linkGenerator = linkGenerator;
        }

        [HttpGet("{addressId:guid}")]
        public async Task<IActionResult> Get(Guid addressId)
        {
            var query = new GetAddressByIdQuery(addressId);
            var result = await _mediator.Send(query);

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
            var query = new GetAddressesQuery(pageNumber, pageSize);
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddAddressDTO addAddressDTO,
            CancellationToken cancellationToken)
        {
            var command = new AddAddressCommand(addAddressDTO.LotNo,
                addAddressDTO.Street,
                addAddressDTO.Barangay,
                addAddressDTO.Municipality,
                addAddressDTO.Region,
                addAddressDTO.Country,
                addAddressDTO.CreatedBy,
                addAddressDTO.IsActive);

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            var linkGenerator = new Dictionary<string, string?>()
            {
                { "ById", _linkGenerator.GenerateLink(HttpContext, "Get", "Address", result.Value) },
                { "List",  _linkGenerator.GenerateLink(HttpContext, "Get", "Address", null) },
                { "Self", _linkGenerator.GenerateLink(HttpContext, "Post", "Address", null) }
            };

            return Ok(linkGenerator);
        }
    }
}
