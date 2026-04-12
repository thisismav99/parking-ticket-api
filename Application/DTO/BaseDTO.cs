using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class BaseDTO
    {
        [Required, MaxLength(100)]
        public string CreatedBy { get; set; } = "direct-api";
        [Required, Range(0,1)]
        public bool IsActive { get; set; } = true;
    }
}
