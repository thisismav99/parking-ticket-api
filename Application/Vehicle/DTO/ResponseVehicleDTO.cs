namespace Application.Vehicle.DTO
{
    internal class ResponseVehicleDTO
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
