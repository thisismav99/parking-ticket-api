using Application.Applications.User.DTO;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.User;
using MediatR;

namespace Application.Applications.User.Command
{
    internal record AddRoleClaimCommand(AddRoleClaimDTO AddRoleClaimDTO) : IRequest<Result<string>>;

    internal class AddRoleClaimCommandHandler : IRequestHandler<AddRoleClaimCommand, Result<string>>
    {
        private readonly IRoleService _roleService;

        public AddRoleClaimCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<Result<string>> Handle(AddRoleClaimCommand request, CancellationToken cancellationToken)
        {
            var roleClaim = await _roleService.AddRoleClaim(request.AddRoleClaimDTO.Role, request.AddRoleClaimDTO.ClaimType, request.AddRoleClaimDTO.ClaimValue);
            
            if (roleClaim.IsFailure)
            {
                return Result.Failure<string>(roleClaim.Error);
            }

            return Result.Success(roleClaim.Value);
        }
    }
}
