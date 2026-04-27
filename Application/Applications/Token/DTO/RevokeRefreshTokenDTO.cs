using System.ComponentModel.DataAnnotations;

namespace Application.Applications.Token.DTO
{
    public class RevokeRefreshTokenDTO
    {
        [Required, MaxLength(200)]
        public required string RefreshToken { get; set; }
    }
}
