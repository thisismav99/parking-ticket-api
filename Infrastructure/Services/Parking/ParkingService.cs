using CSharpFunctionalExtensions;
using Infrastructure.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Infrastructure.Services.Parking
{
    internal interface IParkingService
    {
        Task<Result<Guid>> AddParking(Domain.Entities.Parking.Parking parking);

        Task<Result<Domain.Entities.Parking.Parking?>> GetParkingById(Guid parkingId);

        Task<List<Domain.Entities.Parking.Parking>> GetParkings(int pageNumber, int pageSize);
    }

    internal class ParkingService : IParkingService
    {
        private readonly ParkingTicketDbContext _parkingTicketDbContext;
        private readonly DbSet<Domain.Entities.Parking.Parking> _parkings;

        public ParkingService(ParkingTicketDbContext parkingTicketDbContext)
        {
            _parkingTicketDbContext = parkingTicketDbContext;
            _parkings = _parkingTicketDbContext.Set<Domain.Entities.Parking.Parking>();
        }

        public async Task<Result<Guid>> AddParking(Domain.Entities.Parking.Parking parking)
        {
            if(parking is not null)
            {
                await _parkings.AddAsync(parking);
                await _parkingTicketDbContext.SaveChangesAsync();

                return Result.Success(parking.Id);
            }

            return Result.Failure<Guid>("Parking cannot be null");
        }

        public async Task<Result<Domain.Entities.Parking.Parking?>> GetParkingById(Guid parkingId)
        {
            if(parkingId != Guid.Empty)
            {
                var parking = await _parkings.FirstOrDefaultAsync(p => p.Id == parkingId);
                
                return Result.Success(parking);
            }

            return Result.Failure<Domain.Entities.Parking.Parking?>("Parking Id cannot be empty");
        }

        public async Task<List<Domain.Entities.Parking.Parking>> GetParkings(int pageNumber, int pageSize)
        {
            return await GetPagedList<Domain.Entities.Parking.Parking>.GetList(_parkings, pageNumber, pageSize);
        }
    }
}
