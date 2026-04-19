using Application.Applications.User.Command;
using Application.Applications.User.DTO;
using Application.Applications.User.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketingAPI.Utilities.Helpers;

namespace ParkingTicketingAPI.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private LinkGenerator _linkGenerator;

        public UserController(IMediator mediator,
            LinkGenerator linkGenerator)
        {
            _mediator = mediator;
            _linkGenerator = linkGenerator;
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

            var linkGenerator = new Dictionary<string, string?>()
            {
                { "ById", _linkGenerator.GenerateLink(HttpContext, "Get", "User", result.Value) },
                { "List",  _linkGenerator.GenerateLink<string>(HttpContext, "Get", "User", null) },
                { "Self", _linkGenerator.GenerateLink<string>(HttpContext, "Post", "User", null) },
                { "AddUserRole", _linkGenerator.GenerateLink<string>(HttpContext, "AddUserRole", "User", null) },
                { "AddUserClaim", _linkGenerator.GenerateLink<string>(HttpContext, "AddUserClaim", "User", null) }
            };

            return Ok(linkGenerator);
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

            var linkGenerator = new Dictionary<string, string?>()
            {
                { "ById", _linkGenerator.GenerateLink(HttpContext, "Get", "User", result.Value) },
                { "List",  _linkGenerator.GenerateLink<string>(HttpContext, "Get", "User", null) },
                { "AddUser", _linkGenerator.GenerateLink<string>(HttpContext, "Post", "User", null) },
                { "Self", _linkGenerator.GenerateLink<string>(HttpContext, "AddUserRole", "User", null) },
                { "AddUserClaim", _linkGenerator.GenerateLink<string>(HttpContext, "AddUserClaim", "User", null) }
            };

            return Ok(linkGenerator);
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

            var linkGenerator = new Dictionary<string, string?>()
            {
                { "ById", _linkGenerator.GenerateLink(HttpContext, "Get", "User", result.Value) },
                { "List",  _linkGenerator.GenerateLink<string>(HttpContext, "Get", "User", null) },
                { "AddUser", _linkGenerator.GenerateLink<string>(HttpContext, "Post", "User", null) },
                { "AddUserRole", _linkGenerator.GenerateLink<string>(HttpContext, "AddUserRole", "User", null) },
                { "Self", _linkGenerator.GenerateLink<string>(HttpContext, "AddUserClaim", "User", null) }
            };

            return Ok(linkGenerator);
        }
    }
}
