using Application.Applications.Company.Command;
using Application.Applications.Company.DTO;
using Application.Applications.Company.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketingAPI.Utilities.Helpers;

namespace ParkingTicketingAPI.Controllers.Company
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly LinkGenerator _linkGenerator;

        public CompanyController(IMediator mediator,
            LinkGenerator linkGenerator)
        {
            _mediator = mediator;
            _linkGenerator = linkGenerator;
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

            var linkGenerator = new Dictionary<string, string?>()
            {
                { "ById", _linkGenerator.GenerateLink(HttpContext, "Get", "Company", result.Value) },
                { "List",  _linkGenerator.GenerateLink<string>(HttpContext, "Get", "Company", null) },
                { "Self", _linkGenerator.GenerateLink<string>(HttpContext, "Post", "Company", null) }
            };

            return Ok(linkGenerator);
        }
    }
}
