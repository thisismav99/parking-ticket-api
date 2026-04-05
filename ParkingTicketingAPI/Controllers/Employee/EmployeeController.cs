using Application.Applications.Employee.Command;
using Application.Applications.Employee.DTO;
using Application.Applications.Employee.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketingAPI.Utilities.Helpers;

namespace ParkingTicketingAPI.Controllers.Employee
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly LinkGenerator _linkGenerator;

        public EmployeeController(IMediator mediator,
            LinkGenerator linkGenerator)
        {
            _mediator = mediator;
            _linkGenerator = linkGenerator;
        }

        [HttpGet("{employeeId:guid}")]
        public async Task<IActionResult> Get(Guid employeeId, CancellationToken cancellationToken)
        {
            var query = new GetEmployeeByIdQuery(employeeId);
            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var query = new GetEmployeesQuery(pageNumber, pageSize);
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddEmployeeDTO addEmployeeDTO, CancellationToken cancellationToken)
        {
            var command = new AddEmployeesCommand(addEmployeeDTO.FirstName,
                addEmployeeDTO.MiddleName,
                addEmployeeDTO.LastName,
                addEmployeeDTO.AddressId,
                addEmployeeDTO.CompanyId,
                addEmployeeDTO.CreatedBy,
                addEmployeeDTO.IsActive);

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            var linkGenerator = new Dictionary<string, string?>()
            {
                { "ById", _linkGenerator.GenerateLink(HttpContext, "Get", "Employee", result.Value) },
                { "List",  _linkGenerator.GenerateLink(HttpContext, "Get", "Employee", null) },
                { "Self", _linkGenerator.GenerateLink(HttpContext, "Post", "Employee", null) }
            };

            return Ok(linkGenerator);
        }
    }
}
