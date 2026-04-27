using Application.Applications.Token.DTO;
using Application.Utilities.Helpers;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.Employee;
using Infrastructure.Interfaces.Token;
using Infrastructure.Interfaces.User;
using MediatR;

namespace Application.Applications.Token.Command
{
    internal record GenerateRefreshTokenCommand(GenerateRefreshTokenDTO GenerateRefreshTokenDTO) : IRequest<Result<string>>;

    internal class GenerateRefreshTokenCommandHandler : IRequestHandler<GenerateRefreshTokenCommand, Result<string>>
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IEmployeeService _employeeService;

        public GenerateRefreshTokenCommandHandler(ITokenService tokenService,
            IUserService userService,
            IEmployeeService employeeService)
        {
            _tokenService = tokenService;
            _userService = userService;
            _employeeService = employeeService;
        }

        public async Task<Result<string>> Handle(GenerateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var userEmployee = await _userService.GetUserEmployee(request.GenerateRefreshTokenDTO.UserId, cancellationToken);

            if (userEmployee is null)
            {
                return Result.Failure<string>(GetError.NotFound(request.GenerateRefreshTokenDTO.UserId));
            }

            var employee = await _employeeService.GetEmployeeById(userEmployee.EmployeeId, cancellationToken);

            if (employee is null)
            {
                return Result.Failure<string>(GetError.NotFound(userEmployee.EmployeeId));
            }

            var oldRefreshToken = request.GenerateRefreshTokenDTO.RefreshToken;

            await _tokenService.RevokeRefreshToken(oldRefreshToken, cancellationToken);

            string newRefreshToken = RandomToken.Generate();

            var refreshToken = new Domain.Entities.Token.RefreshToken(
                newRefreshToken,
                false,
                null,
                request.GenerateRefreshTokenDTO.UserId);

            await _tokenService.GenerateRefreshToken(refreshToken, cancellationToken);

            var token = await _tokenService.GenerateToken(userEmployee.IdentityUser!, employee);

            return Result.Success(token);
        }
    }
}
