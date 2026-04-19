using System.ComponentModel.DataAnnotations;

namespace Application.Applications.User.DTO
{
    public class AddRoleClaimDTO
    {
        [Required, MaxLength(30)]
        public required string Role { get; set; }

        [Required, MaxLength(30)]
        public required string ClaimType { get; set; }
        
        [Required, MaxLength(30)]
        public required string ClaimValue { get; set; }
    }
}
