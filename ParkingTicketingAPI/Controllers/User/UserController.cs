using Application.Applications.User.Command;
using Application.Applications.User.DTO;
using Application.Applications.User.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketingAPI.Utilities.Enums;

namespace ParkingTicketingAPI.Controllers.User
{
    public class UserController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator,
            LinkGenerator linkGenerator) : base(linkGenerator)
        {
            _mediator = mediator;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email,
            CancellationToken cancellationToken)
        {
            var query = new GetUserByEmailQuery(email);

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
            var query = new GetUsersQuery(pageNumber, pageSize);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddUserDTO addUserDTO,
            CancellationToken cancellationToken)
        {
            var command = new AddUserCommand(addUserDTO);

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtPostResponse(
             [
                LinkKeys.ById,
                LinkKeys.List,
                LinkKeys.Self,
                LinkKeys.AddUserRole,
                LinkKeys.AddUserClaim
             ], result.Value);
        }

        [HttpPost("/api/[controller]/role")]
        public async Task<IActionResult> AddUserRole(AddUserRoleDTO addUserRoleDTO,
            CancellationToken cancellationToken)
        {
            var command = new AddUserRoleCommand(addUserRoleDTO);

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtPostResponse(
             [
                LinkKeys.ById,
                LinkKeys.List,
                LinkKeys.Self,
                LinkKeys.AddUserRole,
                LinkKeys.AddUserClaim
             ], result.Value);
        }

        [HttpPost("/api/[controller]/claim")]
        public async Task<IActionResult> AddUserClaim(AddUserClaimDTO addUserClaimDTO,
            CancellationToken cancellationToken)
        {
            var command = new AddUserClaimCommand(addUserClaimDTO);

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtPostResponse(
             [
                LinkKeys.ById,
                LinkKeys.List,
                LinkKeys.Self,
                LinkKeys.AddUserRole,
                LinkKeys.AddUserClaim
             ], result.Value);
        }
    }
}
