using Application.Applications.User.DTO;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.User;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Applications.User.Command
{
    internal record AddUserCommand(AddUserDTO AddUserDTO) : IRequest<Result<string>>;

    internal class AddUserCommandHandler : IRequestHandler<AddUserCommand, Result<string>>
    {
        private readonly IUserService _userService;

        public AddUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var identityUser = new IdentityUser
            {
                UserName = request.AddUserDTO.Email,
                Email = request.AddUserDTO.Email,
                PhoneNumber = request.AddUserDTO.PhoneNumber
            };

            var user = await _userService.AddUser(identityUser, 
                request.AddUserDTO.Password, 
                request.AddUserDTO.EmployeeId, 
                cancellationToken);

            if(user.IsFailure)
            {
                return Result.Failure<string>(user.Error);
            }

            return Result.Success(user.Value);
        }
    }
}
