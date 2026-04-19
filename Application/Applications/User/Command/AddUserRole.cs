using Application.Applications.User.DTO;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.User;
using MediatR;

namespace Application.Applications.User.Command
{
    internal record AddUserRoleCommand(AddUserRoleDTO AddUserRoleDTO) : IRequest<Result<string>>;

    internal class AddUserRoleCommandHandler : IRequestHandler<AddUserRoleCommand, Result<string>>
    {
        private readonly IUserService _userService;

        public AddUserRoleCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result<string>> Handle(AddUserRoleCommand request, CancellationToken cancellationToken)
        {
            var userRole = await _userService.AddUserRole(request.AddUserRoleDTO.Email, request.AddUserRoleDTO.Role);
            
            if(userRole.IsFailure)
            {
                return Result.Failure<string>(userRole.Error);
            }

            return Result.Success(userRole.Value);
        }
    }
}
