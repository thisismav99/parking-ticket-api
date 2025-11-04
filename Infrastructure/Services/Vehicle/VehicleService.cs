using CSharpFunctionalExtensions;
using Infrastructure.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Infrastructure.Services.Vehicle
{
    internal interface IVehicleService
    {
        Task<Result<Guid>> AddVehicle(Domain.Entities.Vehicle.Vehicle vehicle);

        Task<List<Domain.Entities.Vehicle.Vehicle>> GetVehicles(int pageNumber, int pageSize);

        Task<Result<Domain.Entities.Vehicle.Vehicle?>> GetVehicleById(Guid vehicleId);
    }

    internal class VehicleService : IVehicleService
    {
        private readonly ParkingTicketDbContext _parkingTicketDbContext;
        private readonly DbSet<Domain.Entities.Vehicle.Vehicle> _vehicles;

        public VehicleService(ParkingTicketDbContext parkingTicketDbContext)
        {
            _parkingTicketDbContext = parkingTicketDbContext;
            _vehicles = _parkingTicketDbContext.Set<Domain.Entities.Vehicle.Vehicle>();
        }

        public async Task<Result<Guid>> AddVehicle(Domain.Entities.Vehicle.Vehicle vehicle)
        {
            if(vehicle is not null)
            {
                await _vehicles.AddAsync(vehicle);
                await _parkingTicketDbContext.SaveChangesAsync();

                return Result.Success(vehicle.Id);
            }

            return Result.Failure<Guid>("Vehicle cannot be null");
        }

        public async Task<Result<Domain.Entities.Vehicle.Vehicle?>> GetVehicleById(Guid vehicleId)
        {
            if(vehicleId != Guid.Empty)
            {
                var vehicle = await _vehicles.FirstOrDefaultAsync(v => v.Id == vehicleId);

                return Result.Success(vehicle);
            }

            return Result.Failure<Domain.Entities.Vehicle.Vehicle?>("Vehicle Id cannot be empty");
        }

        public async Task<List<Domain.Entities.Vehicle.Vehicle>> GetVehicles(int pageNumber, int pageSize)
        {
            return await GetPagedList<Domain.Entities.Vehicle.Vehicle>.GetList(_vehicles, pageNumber, pageSize);
        }
    }
}
