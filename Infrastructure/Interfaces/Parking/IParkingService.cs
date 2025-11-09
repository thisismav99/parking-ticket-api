namespace Infrastructure.Interfaces.Parking
{
    internal interface IParkingService
    {
        Task<Guid> AddParking(Domain.Entities.Parking.Parking parking, CancellationToken cancellationToken);

        Task<Domain.Entities.Parking.Parking?> GetParkingById(Guid parkingId, CancellationToken cancellationToken);

        Task<List<Domain.Entities.Parking.Parking>> GetParkings(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
