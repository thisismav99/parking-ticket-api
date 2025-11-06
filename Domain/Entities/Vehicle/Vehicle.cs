using Domain.Entities.Common;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities.Vehicle
{
    internal sealed class Vehicle : BaseEntity
    {
        public required string PlateNo { get; set; }

        public required string Brand { get; set; }

        public string? Color { get; set; }

        public string? Model { get; set; }

        public bool IsElectric { get; set; }

        public bool IsHybrid { get; set; }

        public Guid? CustomerId { get; set; }

        public Customer.Customer? Customer { get; set; }

        private Vehicle() { }

        [SetsRequiredMembers]
        public Vehicle(string plateNo, string brand, string? color, string? model, bool isElectric, bool isHybrid, Guid? customerId, string createdBy, bool isActive)
        {
            PlateNo = plateNo;
            Brand = brand;
            Color = color;
            Model = model;
            IsElectric = isElectric;
            IsHybrid = isHybrid;
            CustomerId = customerId;
            CreatedBy = createdBy;
            IsActive = isActive;
        }
    }
}
