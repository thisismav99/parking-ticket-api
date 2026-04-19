using Application.Applications.User.Command;
using Application.Applications.User.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketingAPI.Utilities.Helpers;

namespace ParkingTicketingAPI.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private LinkGenerator _linkGenerator;

        public RoleController(IMediator mediator,
            LinkGenerator linkGenerator)
        {
            _mediator = mediator;
            _linkGenerator = linkGenerator;
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

            var linkGenerator = new Dictionary<string, string?>()
            {
                { "AddRoleClaim",  _linkGenerator.GenerateLink<string>(HttpContext, "Claim", "Role", null) },
                { "Self", _linkGenerator.GenerateLink<string>(HttpContext, "Post", "Role", null) }
            };

            return Ok(linkGenerator);
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

            var linkGenerator = new Dictionary<string, string?>()
            {
                { "Self",  _linkGenerator.GenerateLink<string>(HttpContext, "Claim", "Role", null) },
                { "AddRole", _linkGenerator.GenerateLink<string>(HttpContext, "Post", "Role", null) }
            };

            return Ok(linkGenerator);
        }
    }
}
