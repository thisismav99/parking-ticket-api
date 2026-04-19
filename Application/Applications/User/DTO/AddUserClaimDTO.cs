using System.ComponentModel.DataAnnotations;

namespace Application.Applications.User.DTO
{
    public class AddUserClaimDTO
    {
        [Required, EmailAddress, MaxLength(15)]
        public required string Email { get; set; }

        [Required, MaxLength(30)]
        public required string ClaimType { get; set; }

        [Required, MaxLength(30)]
        public required string ClaimValue { get; set; }
    }
}
