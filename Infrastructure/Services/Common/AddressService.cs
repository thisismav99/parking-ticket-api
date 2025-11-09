using Domain.Entities.Common;
using Infrastructure.Interfaces.Common;
using Infrastructure.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Infrastructure.Services.Common
{
    internal class AddressService : IAddressService
    {
        private readonly ParkingTicketDbContext _parkingTicketDbContext;
        private readonly DbSet<Address> _addresses;

        public AddressService(ParkingTicketDbContext parkingTicketDbContext)
        {
            _parkingTicketDbContext = parkingTicketDbContext;
            _addresses = _parkingTicketDbContext.Set<Address>();
        }

        public async Task<Guid> AddAddress(Address address, CancellationToken cancellationToken)
        {
            await _addresses.AddAsync(address, cancellationToken);
            await _parkingTicketDbContext.SaveChangesAsync(cancellationToken);

            return address.Id;
        }

        public async Task<Address?> GetAddressById(Guid addressId, CancellationToken cancellationToken)
        {
            return await _addresses.FirstOrDefaultAsync(a => a.Id == addressId, cancellationToken);
        }

        public async Task<List<Address>> GetAddresses(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await GetPagedList<Address>.GetList(_addresses, pageNumber, pageSize, cancellationToken);
        }
    }
}
