using Infrastructure.Interfaces.Parking;
using Infrastructure.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Infrastructure.Services.Parking
{
    internal class ParkingService : IParkingService
    {
        private readonly ParkingTicketDbContext _parkingTicketDbContext;
        private readonly DbSet<Domain.Entities.Parking.Parking> _parkings;

        public ParkingService(ParkingTicketDbContext parkingTicketDbContext)
        {
            _parkingTicketDbContext = parkingTicketDbContext;
            _parkings = _parkingTicketDbContext.Set<Domain.Entities.Parking.Parking>();
        }

        public async Task<Guid> AddParking(Domain.Entities.Parking.Parking parking, CancellationToken cancellationToken)
        {
            await _parkings.AddAsync(parking, cancellationToken);
            await _parkingTicketDbContext.SaveChangesAsync(cancellationToken);

            return parking.Id;
        }

        public async Task<Domain.Entities.Parking.Parking?> GetParkingById(Guid parkingId, CancellationToken cancellationToken)
        {
            return await _parkings.FirstOrDefaultAsync(p => p.Id == parkingId, cancellationToken);
        }

        public async Task<List<Domain.Entities.Parking.Parking>> GetParkings(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await GetPagedList<Domain.Entities.Parking.Parking>.GetList(_parkings, pageNumber, pageSize, cancellationToken);
        }
    }
}
