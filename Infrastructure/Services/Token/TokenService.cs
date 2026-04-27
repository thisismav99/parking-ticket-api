using CSharpFunctionalExtensions;
using Domain.Entities.Token;
using Infrastructure.Interfaces.Token;
using Infrastructure.Utilities.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence.Contexts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services.Token
{
    internal class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserDbContext _userDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DbSet<RefreshToken> _refreshToken;

        public TokenService(IConfiguration configuration,
            UserDbContext userDbContext,
            UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _userDbContext = userDbContext;
            _userManager = userManager;
            _refreshToken = _userDbContext.Set<RefreshToken>();
        }

        public async Task<string> GenerateRefreshToken(RefreshToken refreshToken,
            CancellationToken cancellationToken)
        {
            await _refreshToken.AddAsync(refreshToken, cancellationToken);
            await _userDbContext.SaveChangesAsync(cancellationToken);

            return refreshToken.Token;
        }

        public async Task<string> GenerateToken(IdentityUser identityUser,
            Domain.Entities.Employee.Employee employee)
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

            var userClaims = await _userManager.GetClaimsAsync(identityUser);
            tokenClaims.AddRange(userClaims);

            var roles = await _userManager.GetRolesAsync(identityUser);
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

        public async Task<Result> RevokeRefreshToken(string refreshToken,
            CancellationToken cancellationToken)
        {
            var token = await _refreshToken.FirstOrDefaultAsync(x => x.Token == refreshToken, cancellationToken);

            if (token is null)
            {
                return Result.Failure(GetError.Error(string.Empty, "Refresh token not found."));
            }

            token.IsRevoked = true;
            token.DateRevoked = DateTime.UtcNow;

            await _userDbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
