using Application.Applications.User.DTO;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.User;
using Mapster;
using MediatR;

namespace Application.Applications.User.Query
{
    internal record GetUsersQuery(int PageNumber, int PageSize) : IRequest<List<ResponseUserDTO>>;

    internal class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<ResponseUserDTO>>
    {
        private readonly IUserService _userService;

        public GetUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<List<ResponseUserDTO>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userService.GetIdentityUsers(request.PageNumber, request.PageSize, cancellationToken);

            List<ResponseUserDTO> userDTOs = users.Adapt<List<ResponseUserDTO>>();

            return userDTOs;
        }
    }
}
