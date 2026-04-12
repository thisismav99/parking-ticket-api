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

        Task<Result<string>> AssignUserRole(string email, string role);

        Task<Result<string>> AssignUserClaim(string email, string claimType, string claimValue);
    }
}
