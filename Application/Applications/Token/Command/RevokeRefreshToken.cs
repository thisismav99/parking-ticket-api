using Application.Applications.Token.DTO;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.Token;
using MediatR;

namespace Application.Applications.Token.Command
{
    internal record RevokeRefreshTokenCommand(RevokeRefreshTokenDTO RevokeRefreshTokenDTO) : IRequest<Result>;

    internal class RevokeRefreshTokenCommandHandler : IRequestHandler<RevokeRefreshTokenCommand, Result>
    {
        private readonly ITokenService _tokenService;

        public RevokeRefreshTokenCommandHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<Result> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _tokenService.RevokeRefreshToken(request.RevokeRefreshTokenDTO.RefreshToken, cancellationToken);

            if(result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            return Result.Success();
        }
    }
}
