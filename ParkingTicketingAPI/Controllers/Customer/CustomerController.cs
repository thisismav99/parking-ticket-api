using Application.Customer.Command;
using Application.Customer.DTO;
using Application.Customer.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketingAPI.Utilities.Helpers;

namespace ParkingTicketingAPI.Controllers.Customer
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly LinkGenerator _linkGenerator;

        public CustomerController(IMediator mediator,
            LinkGenerator linkGenerator)
        {
            _mediator = mediator;
            _linkGenerator = linkGenerator;
        }

        [HttpGet("{customerId:guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid customerId,
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
        public async Task<IActionResult> Get([FromRoute] int pageNumber,
            [FromRoute] int pageSize,
            CancellationToken cancellationToken)
        {
            var query = new GetCustomersQuery(pageNumber, pageSize);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddCustomerDTO addCustomerDTO,
            CancellationToken cancellationToken)
        {
            var command = new AddCustomerCommand(addCustomerDTO.FirstName,
                addCustomerDTO.MiddleName,
                addCustomerDTO.LastName,
                addCustomerDTO.ContactNo,
                addCustomerDTO.Email,
                addCustomerDTO.LotNo,
                addCustomerDTO.Street,
                addCustomerDTO.Barangay,
                addCustomerDTO.Municipality,
                addCustomerDTO.Region,
                addCustomerDTO.Country,
                addCustomerDTO.CreatedBy,
                addCustomerDTO.IsActive
            );

            var result = await _mediator.Send(command, cancellationToken);

            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            var linkGenerator = new Dictionary<string, string?>()
            {
                { "ById", _linkGenerator.GenerateLink(HttpContext, "Get", "Transaction", result.Value) },
                { "List",  _linkGenerator.GenerateLink(HttpContext, "Get", "Transaction", null) },
                { "Self", _linkGenerator.GenerateLink(HttpContext, "Post", "Transaction", null) }
            };

            return Ok(linkGenerator);
        }
    }
}
