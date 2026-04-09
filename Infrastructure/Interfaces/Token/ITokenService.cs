using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Interfaces.Token
{
    internal interface ITokenService
    {
        Task<string> GenerateToken(IdentityUser identityUser, 
            Domain.Entities.Employee.Employee employee,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager);

        string GenerateRefreshToken();

        Task RevokeToken();
    }
}
