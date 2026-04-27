using Application.Applications.Token.Command;
using Application.Applications.Token.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ParkingTicketingAPI.Controllers.Token
{
    public class TokenController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public TokenController(IMediator mediator,
            LinkGenerator linkGenerator)
            : base(linkGenerator)
        {
            _mediator = mediator;
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> GenerateRefreshToken(GenerateRefreshTokenDTO generateRefreshTokenDTO,
            CancellationToken cancellationToken)
        {
            var command = new GenerateRefreshTokenCommand(generateRefreshTokenDTO);
            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result);
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeRefreshToken(RevokeRefreshTokenDTO revokeRefreshTokenDTO,
            CancellationToken cancellationToken)
        {
            var command = new RevokeRefreshTokenCommand(revokeRefreshTokenDTO);
            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }
    }
}
