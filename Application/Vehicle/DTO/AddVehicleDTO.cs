using System.ComponentModel.DataAnnotations;

namespace Application.Vehicle.DTO
{
    internal class AddVehicleDTO
    {
        [Required]
        [MaxLength(10)]
        public required string PlateNo { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Brand { get; set; }

        [MaxLength(30)]
        public string? Color { get; set; }

        [MaxLength(50)]
        public string? Model { get; set; }

        [Required]
        public bool IsElectric { get; set; }

        [Required]
        public bool IsHybrid { get; set; }

        public Guid? CustomerId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string CreatedBy { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
