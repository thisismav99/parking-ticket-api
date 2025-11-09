using Domain.Entities.Common;

namespace Infrastructure.Interfaces.Common
{
    internal interface IAddressService
    {
        Task<Guid> AddAddress(Address address, CancellationToken cancellationToken);

        Task<Address?> GetAddressById(Guid addressId, CancellationToken cancellationToken);

        Task<List<Address>> GetAddresses(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
