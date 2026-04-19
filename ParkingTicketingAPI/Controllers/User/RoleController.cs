using Application.Applications.User.Command;
using Application.Applications.User.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketingAPI.Utilities.Enums;

namespace ParkingTicketingAPI.Controllers.User
{
    public class RoleController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator,
            LinkGenerator linkGenerator) : base(linkGenerator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddRoleDTO addRoleDTO,
            CancellationToken cancellationToken)
        {
            var command = new AddRoleCommand(addRoleDTO);

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtPostResponse(
            [
                LinkKeys.AddRoleClaim,
                LinkKeys.AddRole
            ], result.Value);
        }

        [HttpPost("/api/[controller]/claim")]
        public async Task<IActionResult> Claim(AddRoleClaimDTO addRoleClaimDTO,
            CancellationToken cancellationToken)
        {
            var command = new AddRoleClaimCommand(addRoleClaimDTO);

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtPostResponse(
            [
                LinkKeys.AddRoleClaim,
                LinkKeys.AddRole
            ], result.Value);
        }
    }
}
