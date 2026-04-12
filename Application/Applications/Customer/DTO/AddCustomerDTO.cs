using Application.DTO;
using System.ComponentModel.DataAnnotations;

namespace Application.Applications.Customer.DTO
{
    public class AddCustomerDTO : BaseDTO
    {
        [Required, MaxLength(50)]
        public required string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string? MiddleName { get; set; }

        [Required, MaxLength(50)]
        public required string LastName { get; set; }

        [MaxLength(15)]
        public string? ContactNo { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        [Required]
        public Guid AddressId { get; set; }
    }
}
