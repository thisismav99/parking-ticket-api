namespace Infrastructure.Interfaces.Customer
{
    internal interface ICustomerService
    {
        Task<Guid> AddCustomer(Domain.Entities.Customer.Customer customer, 
            Domain.Entities.Common.Address address,
            CancellationToken cancellationToken);

        Task<List<Domain.Entities.Customer.Customer>> GetCustomers(int pageNumber, int pageSize, CancellationToken cancellationToken);

        Task<Domain.Entities.Customer.Customer?> GetCustomerById(Guid customerId, CancellationToken cancellationToken);
    }
}
