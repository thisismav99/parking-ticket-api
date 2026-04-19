using Application.Applications.Customer.Command;
using Application.Applications.Customer.DTO;
using Application.Applications.Customer.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketingAPI.Utilities.Enums;

namespace ParkingTicketingAPI.Controllers.Customer
{
    public class CustomerController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator,
            LinkGenerator linkGenerator) : base(linkGenerator)
        {
            _mediator = mediator;
        }

        [HttpGet("{customerId:guid}")]
        public async Task<IActionResult> Get(Guid customerId,
            CancellationToken cancellationToken)
        {
            var query = new GetCustomerByIdQuery(customerId);

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
            var query = new GetCustomersQuery(pageNumber, pageSize);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddCustomerDTO addCustomerDTO,
            CancellationToken cancellationToken)
        {
            var command = new AddCustomerCommand(addCustomerDTO);

            var result = await _mediator.Send(command, cancellationToken);

            if(result.IsFailure)
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
