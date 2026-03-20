using Application.Parking.Command;
using Application.Parking.DTO;
using Application.Parking.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketingAPI.Utilities.Helpers;

namespace ParkingTicketingAPI.Controllers.Parking
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly LinkGenerator _linkGenerator;

        public TransactionController(IMediator mediator,
            LinkGenerator linkGenerator)
        {
            _mediator = mediator;
            _linkGenerator = linkGenerator;
        }

        [HttpGet("{transactionId:guid}")]
        public async Task<IActionResult> Get([FromRoute]Guid transactionId,
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
        public async Task<IActionResult> Get([FromRoute]int pageNumber,
            [FromRoute]int pageSize,
            CancellationToken cancellationToken)
        {
            var query = new GetTransactionsQuery(pageNumber, pageSize);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddTransactionDTO addTransactionDTO,
            CancellationToken cancellationToken)
        {
            var command = new AddTransactionCommand(addTransactionDTO.AmountToPay,
                addTransactionDTO.AmountPaid,
                addTransactionDTO.IsCard,
                addTransactionDTO.IsCash,
                addTransactionDTO.CreateadBy,
                addTransactionDTO.IsActive);

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
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
