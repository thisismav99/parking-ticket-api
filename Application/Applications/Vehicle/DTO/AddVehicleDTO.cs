using Application.DTO;

namespace Application.Applications.Vehicle.DTO
{
    public class AddVehicleDTO : BaseDTO
    {
        public required string PlateNo { get; set; }

        public required string Brand { get; set; }

        public string? Color { get; set; }

        public string? Model { get; set; }

        public bool IsElectric { get; set; }

        public bool IsHybrid { get; set; }

        public Guid? CustomerId { get; set; }
    }
}
