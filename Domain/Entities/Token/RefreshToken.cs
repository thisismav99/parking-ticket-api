using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities.Token
{
    internal sealed class RefreshToken
    {
        public Guid Id { get; set; }

        public required string Token { get; set; }

        public bool IsExpired { get; set; }

        public DateTime DateExpires { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime? DateRevoked { get; set; }

        public required string UserId { get; set; }

        public IdentityUser? IdentityUser { get; set; }

        private RefreshToken() { }

        [SetsRequiredMembers]
        public RefreshToken(string token, bool isRevoked, DateTime? dateRevoked, string userId)
        {
            Token = token;
            IsRevoked = isRevoked;
            DateRevoked = dateRevoked;
            UserId = userId;
        }
    }
}
