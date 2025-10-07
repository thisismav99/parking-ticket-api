using Domain.Entities.Common;

namespace Domain.Entities.Parking
{
    internal sealed class Parking : BaseEntity
    {
        public DateTime ParkDateTime { get; set; }

        public DateTime? ExitDateTime { get; set; }

        public double TotalHours { get; set; }

        public Guid VehicleId { get; set; }

        public Guid EmployeeId { get; set; }

        public Guid TransactionId { get; set; }

        public required Vehicle.Vehicle Vehicle { get; set; }

        public required Employee.Employee Employee { get; set; }

        public required Transaction Transaction { get; set; }
    }
}
