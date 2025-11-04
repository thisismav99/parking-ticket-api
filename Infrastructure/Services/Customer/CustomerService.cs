using CSharpFunctionalExtensions;
using Infrastructure.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Infrastructure.Services.Customer
{
    internal interface ICustomerService
    {
        Task<Result<Guid>> AddCustomer(Domain.Entities.Customer.Customer customer);

        Task<List<Domain.Entities.Customer.Customer>> GetCustomers(int pageNumber, int pageSize);

        Task<Result<Domain.Entities.Customer.Customer?>> GetCustomerById(Guid customerId);
    }

    internal class CustomerService : ICustomerService
    {
        private readonly ParkingTicketDbContext _parkingTicketDbContext;
        private readonly DbSet<Domain.Entities.Customer.Customer> _customers;

        public CustomerService(ParkingTicketDbContext parkingTicketDbContext)
        {
            _parkingTicketDbContext = parkingTicketDbContext;
            _customers = _parkingTicketDbContext.Set<Domain.Entities.Customer.Customer>();
        }

        public async Task<Result<Guid>> AddCustomer(Domain.Entities.Customer.Customer customer)
        {
            if(customer is not null)
            {
                await _customers.AddAsync(customer);
                await _parkingTicketDbContext.SaveChangesAsync();

                return Result.Success(customer.Id);
            }

            return Result.Failure<Guid>("Customer cannot be null");
        }

        public async Task<Result<Domain.Entities.Customer.Customer?>> GetCustomerById(Guid customerId)
        {
            if(customerId != Guid.Empty)
            {
                var customer = await _customers.FirstOrDefaultAsync(c => c.Id == customerId);

                return Result.Success(customer);
            }

            return Result.Failure<Domain.Entities.Customer.Customer?>("Customer Id cannot be empty");
        }

        public async Task<List<Domain.Entities.Customer.Customer>> GetCustomers(int pageNumber, int pageSize)
        {
            return await GetPagedList<Domain.Entities.Customer.Customer>.GetList(_customers, pageNumber, pageSize);
        }
    }
}
