using Application.Applications.Employee.Command;
using Application.Applications.Employee.DTO;
using Application.Applications.Employee.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketingAPI.Utilities.Enums;

namespace ParkingTicketingAPI.Controllers.Employee
{
    public class EmployeeController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator,
            LinkGenerator linkGenerator) : base(linkGenerator)
        {
            _mediator = mediator;
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
            var command = new AddEmployeeCommand(addEmployeeDTO);

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
