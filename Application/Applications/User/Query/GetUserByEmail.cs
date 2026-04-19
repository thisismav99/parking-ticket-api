using Application.Applications.User.DTO;
using Application.Utilities.Helpers;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.User;
using Mapster;
using MediatR;

namespace Application.Applications.User.Query
{
    internal record GetUserByEmailQuery(string Email) : IRequest<Result<ResponseUserDTO?>>;

    internal class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, Result<ResponseUserDTO?>>
    {
        private readonly IUserService _userService;

        public GetUserByEmailQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result<ResponseUserDTO?>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetIdentityUserByEmail(request.Email, cancellationToken);

            if(user is null)
            {
                return Result.Failure<ResponseUserDTO?>(GetError.NotFound(request.Email));
            }
            
            ResponseUserDTO? userDTO = user.Adapt<ResponseUserDTO?>();

            return Result.Success(userDTO);
        }
    }
}
