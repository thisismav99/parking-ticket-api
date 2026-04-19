using Application.Applications.Parking.Command;
using Application.Applications.Parking.DTO;
using Application.Applications.Parking.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketingAPI.Utilities.Enums;

namespace ParkingTicketingAPI.Controllers.Parking
{
    [Route("api/parking/[controller]")]
    public class TransactionController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator,
            LinkGenerator linkGenerator) : base(linkGenerator)
        {
            _mediator = mediator;
        }

        [HttpGet("{transactionId:guid}")]
        public async Task<IActionResult> Get(Guid transactionId,
            CancellationToken cancellationToken)
        {
            var query = new GetTransactionByIdQuery(transactionId);

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
            var query = new GetTransactionsQuery(pageNumber, pageSize);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddTransactionDTO addTransactionDTO,
            CancellationToken cancellationToken)
        {
            var command = new AddTransactionCommand(addTransactionDTO);

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
