using Application.Applications.User.DTO;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.User;
using MediatR;

namespace Application.Applications.User.Command
{
    internal record AddRoleCommand(AddRoleDTO AddRoleDTO) : IRequest<Result<string>>;

    internal class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, Result<string>>
    {
        private readonly IRoleService _roleService;

        public AddRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<Result<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleService.AddRole(request.AddRoleDTO.Role);

            if(role.IsFailure)
            {
                return Result.Failure<string>(role.Error);
            }

            return Result.Success(role.Value);
        }
    }
}
