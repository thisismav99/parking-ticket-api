using Application.DTO;
using System.ComponentModel.DataAnnotations;

namespace Application.Applications.Company.DTO
{
    public class AddCompanyDTO : BaseDTO
    {
        [Required, MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
