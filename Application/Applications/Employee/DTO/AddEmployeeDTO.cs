using Application.DTO;
using System.ComponentModel.DataAnnotations;

namespace Application.Applications.Employee.DTO
{
    public class AddEmployeeDTO : BaseDTO
    {
        [Required, MaxLength(50)]
        public required string FirstName { get; set; }

        [MaxLength(50)]
        public string? MiddleName { get; set; }

        [Required, MaxLength(50)]
        public required string LastName { get; set; }

        [Required]
        public Guid AddressId { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
    }
}
