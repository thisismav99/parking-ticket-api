using Infrastructure.Interfaces.Customer;
using Infrastructure.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Infrastructure.Services.Customer
{
    internal class CustomerService : ICustomerService
    {
        private readonly ParkingTicketDbContext _parkingTicketDbContext;
        private readonly DbSet<Domain.Entities.Common.Address> _customerAddress;
        private readonly DbSet<Domain.Entities.Customer.Customer> _customers;

        public CustomerService(ParkingTicketDbContext parkingTicketDbContext)
        {
            _parkingTicketDbContext = parkingTicketDbContext;
            _customerAddress = _parkingTicketDbContext.Set<Domain.Entities.Common.Address>();
            _customers = _parkingTicketDbContext.Set<Domain.Entities.Customer.Customer>();
        }

        public async Task<Guid> AddCustomer(Domain.Entities.Customer.Customer customer,
            Domain.Entities.Common.Address address,
            CancellationToken cancellationToken)
        {
            await _customerAddress.AddAsync(address, cancellationToken);
            await _customers.AddAsync(customer, cancellationToken);

            await _parkingTicketDbContext.SaveChangesAsync(cancellationToken);

            return customer.Id;
        }

        public async Task<Domain.Entities.Customer.Customer?> GetCustomerById(Guid customerId, CancellationToken cancellationToken)
        {
            return await _customers.FirstOrDefaultAsync(c => c.Id == customerId, cancellationToken);
        }

        public async Task<List<Domain.Entities.Customer.Customer>> GetCustomers(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await GetPagedList<Domain.Entities.Customer.Customer>.GetList(_customers, pageNumber, pageSize, cancellationToken);
        }
    }
}
