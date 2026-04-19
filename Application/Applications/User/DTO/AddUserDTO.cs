using System.ComponentModel.DataAnnotations;

namespace Application.Applications.User.DTO
{
    public class AddUserDTO
    {
        [Required, EmailAddress, MaxLength(30)]
        public required string Email { get; set; }

        [Required, Phone, MaxLength(15)]
        public required string PhoneNumber { get; set; }

        [Required, MaxLength(15)]
        public required string Password { get; set; }

        [Required]
        public Guid EmployeeId { get; set; }
    }
}
