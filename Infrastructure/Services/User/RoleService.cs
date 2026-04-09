using Infrastructure.Interfaces.User;
using Infrastructure.Utilities.Helpers;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Infrastructure.Services.User
{
    internal class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<(string?, List<string>?)> AddRole(string role)
        {
            bool roleExists = await _roleManager.RoleExistsAsync(role);

            if (!roleExists)
            {
                var identityRole = new IdentityRole
                {
                    Name = role,
                    NormalizedName = role.ToUpper()
                };

                var result = await _roleManager.CreateAsync(identityRole);

                if (result.Errors.Any())
                {
                    return (null, result.Errors.Select(e => GetError.Error(e.Code, e.Description)).ToList());
                }

                return (identityRole.Id, null);
            }

            return (null, new List<string>(){ GetError.Error(string.Empty, $"{role} already exists") });
        }

        public async Task<(string?, List<string>?)> AddRoleClaim(string role, string claimType, string claimValue)
        {
            var roleEntity = await _roleManager.FindByNameAsync(role);

            if (roleEntity is not null)
            {
                var roleClaim = await _roleManager.AddClaimAsync(roleEntity, new Claim(claimType, claimValue));

                if (roleClaim.Errors.Any())
                {
                    return (null, roleClaim.Errors.Select(e => GetError.Error(e.Code, e.Description)).ToList());
                }

                return (roleEntity.Id, null);
            }

            return (null, new List<string>() { GetError.Error(string.Empty, $"{role} not found") });
        }
    }
}
