using Application.DTO;
using System.ComponentModel.DataAnnotations;

namespace Application.Applications.Parking.DTO
{
    public class AddParkingDTO : BaseDTO
    {
        [Required]
        public DateTime ParkDateTime { get; set; }

        public DateTime? ExitDateTime { get; set; }

        [Required]
        public Guid VehicleId { get; set; }

        [Required]
        public Guid EmployeeId { get; set; }
    }
}
