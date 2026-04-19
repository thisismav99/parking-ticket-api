using Application.Applications.Company.Command;
using Application.Applications.Company.DTO;
using Application.Applications.Company.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketingAPI.Utilities.Enums;

namespace ParkingTicketingAPI.Controllers.Company
{
    public class CompanyController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator,
            LinkGenerator linkGenerator) : base(linkGenerator)
        {
            _mediator = mediator;
        }

        [HttpGet("{companyId:guid}")]
        public async Task<IActionResult> Get(Guid companyId,
            CancellationToken cancellationToken)
        {
            var query = new GetCompanyByIdQuery(companyId);

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
            var query = new GetCompaniesQuery(pageNumber, pageSize);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddCompanyDTO addCompanyDTO,
            CancellationToken cancellationToken)
        {
            var command = new AddCompanyCommand(addCompanyDTO);

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
