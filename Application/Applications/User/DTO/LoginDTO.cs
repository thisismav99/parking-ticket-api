using System.ComponentModel.DataAnnotations;

namespace Application.Applications.User.DTO
{
    public class LoginDTO
    {
        [Required, EmailAddress, MaxLength(30)]
        public required string Email { get; set; }

        [Required, MaxLength(15)]
        public required string Password { get; set; }
    }
}
