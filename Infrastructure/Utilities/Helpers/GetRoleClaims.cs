using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.Security.Claims;

namespace Infrastructure.Utilities.Helpers
{
    internal static class GetRoleClaims
    {
        public static async Task<List<Claim>> RoleClaims(UserDbContext userDbContext,
            IList<string> roles)
        {
            var roleClaims = await(
                from r in userDbContext.Roles
                join rc in userDbContext.RoleClaims on r.Id equals rc.RoleId
                where roles.Contains(r.Name!)
                select new Claim(rc.ClaimType!, rc.ClaimValue!))
                .ToListAsync();

            return roleClaims;
        }
    }
}
