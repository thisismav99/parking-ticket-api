using System.ComponentModel.DataAnnotations;

namespace Application.Applications.User.DTO
{
    public class AddRoleDTO
    {
        [Required, MaxLength(30)]
        public required string Role { get; set; }
    }
}
