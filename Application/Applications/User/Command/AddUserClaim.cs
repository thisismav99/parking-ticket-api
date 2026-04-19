using Application.Applications.User.DTO;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.User;
using MediatR;

namespace Application.Applications.User.Command
{
    internal record AddUserClaimCommand(AddUserClaimDTO AddUserClaimDTO) : IRequest<Result<string>>;

    internal class AddUserClaimCommandHandler : IRequestHandler<AddUserClaimCommand, Result<string>>
    {
        private readonly IUserService _userService;

        public AddUserClaimCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result<string>> Handle(AddUserClaimCommand request, CancellationToken cancellationToken)
        {
            var userClaim = await _userService.AddUserClaim(request.AddUserClaimDTO.Email, 
                request.AddUserClaimDTO.ClaimType, 
                request.AddUserClaimDTO.ClaimValue);

            if (userClaim.IsFailure)
            {
                return Result.Failure<string>(userClaim.Error);
            }

            return Result.Success(userClaim.Value);
        }
    }
}
