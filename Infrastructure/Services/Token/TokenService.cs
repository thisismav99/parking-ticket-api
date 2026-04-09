using Infrastructure.Interfaces.Token;
using Infrastructure.Utilities.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence.Contexts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services.Token
{
    internal class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserDbContext _userDbContext;

        public TokenService(IConfiguration configuration,
            UserDbContext userDbContext)
        {
            _configuration = configuration;
            _userDbContext = userDbContext;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);

            string refreshToken = Convert.ToBase64String(randomNumber);

            return refreshToken;
        }

        public async Task<string> GenerateToken(IdentityUser identityUser,
            Domain.Entities.Employee.Employee employee,
            RoleManager<IdentityRole> roleManager, 
            UserManager<IdentityUser> userManager)
        {
            var jwt = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenClaims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, identityUser.Id),
                new Claim(JwtRegisteredClaimNames.Email, identityUser.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.GivenName, employee.FirstName),
                new Claim(JwtRegisteredClaimNames.MiddleName, employee.MiddleName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.FamilyName, employee.LastName),
                new Claim(JwtRegisteredClaimNames.PhoneNumber, identityUser.PhoneNumber ?? string.Empty)
            };

            var userClaims = await userManager.GetClaimsAsync(identityUser);
            tokenClaims.AddRange(userClaims);

            var roles = await userManager.GetRolesAsync(identityUser);
            tokenClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var roleClaims = await GetRoleClaims.RoleClaims(_userDbContext, roles);
            tokenClaims.AddRange(roleClaims);

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: tokenClaims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwt["ExpiresInMinutes"]!)),
                signingCredentials: credentials
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }

        public Task RevokeToken()
        {
            throw new NotImplementedException();
        }
    }
}
