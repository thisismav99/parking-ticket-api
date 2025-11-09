using Infrastructure.Interfaces.Vehicle;
using Infrastructure.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Infrastructure.Services.Vehicle
{
    internal class VehicleService : IVehicleService
    {
        private readonly ParkingTicketDbContext _parkingTicketDbContext;
        private readonly DbSet<Domain.Entities.Vehicle.Vehicle> _vehicles;

        public VehicleService(ParkingTicketDbContext parkingTicketDbContext)
        {
            _parkingTicketDbContext = parkingTicketDbContext;
            _vehicles = _parkingTicketDbContext.Set<Domain.Entities.Vehicle.Vehicle>();
        }

        public async Task<Guid> AddVehicle(Domain.Entities.Vehicle.Vehicle vehicle, CancellationToken cancellationToken)
        {
            await _vehicles.AddAsync(vehicle, cancellationToken);
            await _parkingTicketDbContext.SaveChangesAsync(cancellationToken);

            return vehicle.Id;
        }

        public async Task<Domain.Entities.Vehicle.Vehicle?> GetVehicleById(Guid vehicleId, CancellationToken cancellationToken)
        {
            return await _vehicles.FirstOrDefaultAsync(v => v.Id == vehicleId, cancellationToken);
        }

        public async Task<List<Domain.Entities.Vehicle.Vehicle>> GetVehicles(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await GetPagedList<Domain.Entities.Vehicle.Vehicle>.GetList(_vehicles, pageNumber, pageSize, cancellationToken);
        }
    }
}
