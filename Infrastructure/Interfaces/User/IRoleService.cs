using CSharpFunctionalExtensions;

namespace Infrastructure.Interfaces.User
{
    internal interface IRoleService
    {
        Task<Result<string>> AddRole(string role);

        Task<Result<string>> AddRoleClaim(string role, string claimType, string claimValue);
    }
}
