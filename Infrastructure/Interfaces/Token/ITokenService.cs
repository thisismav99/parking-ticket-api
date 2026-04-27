using CSharpFunctionalExtensions;
using Domain.Entities.Token;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Interfaces.Token
{
    internal interface ITokenService
    {
        Task<string> GenerateToken(IdentityUser identityUser, 
            Domain.Entities.Employee.Employee employee);

        Task<string> GenerateRefreshToken(RefreshToken refreshToken, CancellationToken cancellationToken);

        Task<Result> RevokeRefreshToken(string refreshToken, CancellationToken cancellationToken);
    }
}
