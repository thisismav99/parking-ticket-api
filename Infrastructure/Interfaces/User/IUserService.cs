using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Interfaces.User
{
    internal interface IUserService
    {
        Task<Result<string>> AddUser(IdentityUser identityUser, 
            string password, 
            Guid employeeId, 
            CancellationToken cancellationToken);

        Task<IdentityUser?> GetIdentityUserByEmail(string email, CancellationToken cancellationToken);

        Task<List<IdentityUser>> GetIdentityUsers(int pageNumber, int pageSize, CancellationToken cancellationToken);

        Task<Result<string>> AddUserRole(string email, string role);

        Task<Result<string>> AddUserClaim(string email, string claimType, string claimValue);
    }
}
