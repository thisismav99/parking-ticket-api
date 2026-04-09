namespace Infrastructure.Interfaces.User
{
    internal interface IRoleService
    {
        Task<(string?, List<string>?)> AddRole(string role);

        Task<(string?, List<string>?)> AddRoleClaim(string role, string claimType, string claimValue);
    }
}
