using Application.Applications.Common.Command;
using Application.Applications.Common.DTO;
using Application.Applications.Common.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketingAPI.Utilities.Enums;

namespace ParkingTicketingAPI.Controllers.Common
{
    public class AddressController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public AddressController(IMediator mediator,
            LinkGenerator linkGenerator) : base(linkGenerator)
        {
            _mediator = mediator;
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
            var command = new AddAddressCommand(addAddressDTO);

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
