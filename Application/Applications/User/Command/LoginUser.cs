using Application.Applications.User.DTO;
using Application.Utilities.Helpers;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.Employee;
using Infrastructure.Interfaces.Token;
using Infrastructure.Interfaces.User;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Applications.User.Command
{
    internal record LoginUserCommand(LoginDTO LoginDTO) : IRequest<Result<ResponseLoginDTO>>;

    internal class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<ResponseLoginDTO>>
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserService _userService;
        private readonly IEmployeeService _employeeService;

        public LoginUserCommandHandler(ITokenService tokenService,
            UserManager<IdentityUser> userManager,
            IUserService userService,
            IEmployeeService employeeService)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _userService = userService;
            _employeeService = employeeService;
        }

        public async Task<Result<ResponseLoginDTO>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.LoginDTO.Email);

            if(user is null)
            {
                return Result.Failure<ResponseLoginDTO>(GetError.InvalidEmail(request.LoginDTO.Email));
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(user, request.LoginDTO.Password);

            if(!isValidPassword)
            {
                return Result.Failure<ResponseLoginDTO>(GetError.InvalidPassword());
            }

            var employeeId = await _userService.GetUserEmployee(user.Id, cancellationToken);

            if (employeeId is null)
            {
                return Result.Failure<ResponseLoginDTO>(GetError.NotFound(user.Id));
            }

            var employee = await _employeeService.GetEmployeeById(employeeId.EmployeeId, cancellationToken);

            if (employee is null)
            {
                return Result.Failure<ResponseLoginDTO>(GetError.NotFound(employeeId.EmployeeId));
            }

            var token = await _tokenService.GenerateToken(user, employee);
            var refreshToken = RandomToken.Generate();

            var refreshTokenEntity = new Domain.Entities.Token.RefreshToken(
                refreshToken,
                false,
                null,
                user.Id);

            var refreshTokenResult = await _tokenService.GenerateRefreshToken(refreshTokenEntity, cancellationToken);

            return Result.Success(new ResponseLoginDTO
            {
                Token = token,
                RefreshToken = refreshTokenResult
            });
        }
    }
}
