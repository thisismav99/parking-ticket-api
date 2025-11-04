using CSharpFunctionalExtensions;
using Domain.Entities.Common;
using Infrastructure.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Infrastructure.Services.Common
{
    internal interface IAddressService
    {
        Task<Result<Guid>> AddAddress(Address address);

        Task<Result<Address?>> GetAddressById(Guid addressId);

        Task<List<Address>> GetAddresses(int pageNumber, int pageSize);
    }

    internal class AddressService : IAddressService
    {
        private readonly ParkingTicketDbContext _parkingTicketDbContext;
        private readonly DbSet<Address> _addresses;

        public AddressService(ParkingTicketDbContext parkingTicketDbContext)
        {
            _parkingTicketDbContext = parkingTicketDbContext;
            _addresses = _parkingTicketDbContext.Set<Address>();
        }

        public async Task<Result<Guid>> AddAddress(Address address)
        {
            if(address is not null)
            {
                await _addresses.AddAsync(address);
                await _parkingTicketDbContext.SaveChangesAsync();

                return Result.Success(address.Id);
            }

            return Result.Failure<Guid>("Address cannot be null");
        }

        public async Task<Result<Address?>> GetAddressById(Guid addressId)
        {
            if(addressId != Guid.Empty)
            {
                var address = await _addresses.FirstOrDefaultAsync(a => a.Id == addressId);
                
                return Result.Success(address);
            }

            return Result.Failure<Address?>("Address Id cannot be empty");
        }

        public async Task<List<Address>> GetAddresses(int pageNumber, int pageSize)
        {
            return await GetPagedList<Address>.GetList(_addresses, pageNumber, pageSize);
        }
    }
}
