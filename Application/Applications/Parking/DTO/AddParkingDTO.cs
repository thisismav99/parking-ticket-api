using Application.DTO;

namespace Application.Applications.Parking.DTO
{
    public class AddParkingDTO : BaseDTO
    {
        public DateTime ParkDateTime { get; set; }

        public DateTime? ExitDateTime { get; set; }

        public Guid VehicleId { get; set; }

        public Guid EmployeeId { get; set; }

        public Guid TransactionId { get; set; }
    }
}
