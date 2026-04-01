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

        public Vehicle.Vehicle? Vehicle { get; set; }

        public Employee.Employee? Employee { get; set; }

        public Transaction? Transaction { get; set; }
    }
}
