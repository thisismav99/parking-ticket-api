using Domain.Entities.Common;
using Domain.Utilities.CustomException;
using System.Diagnostics.CodeAnalysis;

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

        private Parking() { }

        [SetsRequiredMembers]
        public Parking(DateTime parkDateTime, 
            DateTime? exitDateTime,
            Guid vehicleId,
            Guid employeeId,
            Guid transactionId,
            string createdBy,
            bool isActive)
        {
            ParkDateTime = parkDateTime;
            ExitDateTime = exitDateTime;
            SetVehicleId(vehicleId);
            SetEmployeeId(employeeId);
            SetTransactionId(transactionId);
            CreatedBy = createdBy;
            IsActive = isActive;
        }

        private void SetVehicleId(Guid vehicleId)
        {
            if (VehicleId == Guid.Empty)
            {
                throw new DomainException("VehicleId cannot be empty.");
            }

            VehicleId = vehicleId;
        }

        private void SetEmployeeId(Guid employeeId)
        {
            if (EmployeeId == Guid.Empty)
            {
                throw new DomainException("EmployeeId cannot be empty.");
            }
            EmployeeId = employeeId;
        }

        private void SetTransactionId(Guid transactionId)
        {
            if (TransactionId == Guid.Empty)
            {
                throw new DomainException("TransactionId cannot be empty.");
            }
            TransactionId = transactionId;
        }
    }
}
