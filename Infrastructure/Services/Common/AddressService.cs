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

        public async Task DeleteAddress(Guid addressId, CancellationToken cancellationToken)
        {
            var address = await _addresses.FirstOrDefaultAsync(x => x.Id == addressId, cancellationToken);

            if (address is not null)
            {
                _addresses.Remove(address);
                await _parkingTicketDbContext.SaveChangesAsync(cancellationToken);
            }

            throw new ArgumentNullException($"No address found for {addressId}");
        }

        public async Task<Address?> GetAddressById(Guid addressId, CancellationToken cancellationToken)
        {
            return await _addresses.FirstOrDefaultAsync(a => a.Id == addressId, cancellationToken);
        }

        public async Task<List<Address>> GetAddresses(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await GetPagedList<Address>.GetList(_addresses, pageNumber, pageSize, cancellationToken);
        }

        public async Task UpdateAddress(Address address, CancellationToken cancellationToken)
        {
            _addresses.Attach(address);
            _parkingTicketDbContext.Entry(address).State = EntityState.Modified;

            await _parkingTicketDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
