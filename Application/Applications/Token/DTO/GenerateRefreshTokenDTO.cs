using System.ComponentModel.DataAnnotations;

namespace Application.Applications.Token.DTO
{
    public class GenerateRefreshTokenDTO
    {
        [Required, MaxLength(200)]
        public required string RefreshToken { get; set; }

        [Required, MaxLength(36)]
        public required string UserId { get; set; }
    }
}
