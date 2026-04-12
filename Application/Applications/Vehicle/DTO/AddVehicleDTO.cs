using Application.DTO;
using System.ComponentModel.DataAnnotations;

namespace Application.Applications.Vehicle.DTO
{
    public class AddVehicleDTO : BaseDTO
    {
        [Required, MaxLength(10)]
        public required string PlateNo { get; set; }
        
        [Required, MaxLength(50)]
        public required string Brand { get; set; }

        [MaxLength(30)]
        public string? Color { get; set; }

        [MaxLength(50)]
        public string? Model { get; set; }

        [Required, Range(0, 1)]
        public bool IsElectric { get; set; }

        [Required, Range(0, 1)]
        public bool IsHybrid { get; set; }

        public Guid? CustomerId { get; set; }
    }
}
