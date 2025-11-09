namespace Infrastructure.Interfaces.Vehicle
{
    internal interface IVehicleService
    {
        Task<Guid> AddVehicle(Domain.Entities.Vehicle.Vehicle vehicle, CancellationToken cancellationToken);

        Task<List<Domain.Entities.Vehicle.Vehicle>> GetVehicles(int pageNumber, int pageSize, CancellationToken cancellationToken);

        Task<Domain.Entities.Vehicle.Vehicle?> GetVehicleById(Guid vehicleId, CancellationToken cancellationToken);
    }
}
