using System.ComponentModel.DataAnnotations;

namespace Application.Applications.User.DTO
{
    public class AddUserRoleDTO
    {
        [Required, EmailAddress, MaxLength(30)]
        public required string Email { get; set; }

        [Required, MaxLength(30)]
        public required string Role { get; set; }
    }
}
