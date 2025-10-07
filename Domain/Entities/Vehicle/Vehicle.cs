using Domain.Entities.Common;

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
    }
}
