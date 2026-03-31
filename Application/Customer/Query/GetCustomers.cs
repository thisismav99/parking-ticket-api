using Application.Customer.DTO;
using Infrastructure.Interfaces.Customer;
using Mapster;
using MediatR;

namespace Application.Customer.Query
{
    internal record class GetCustomersQuery(int PageNumber, int PageSize) : IRequest<List<ResponseCustomerDTO>>;

    internal class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<ResponseCustomerDTO>>
    {
        private readonly ICustomerService _customerService;

        public GetCustomersQueryHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<List<ResponseCustomerDTO>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customerService.GetCustomers(request.PageNumber, request.PageSize, cancellationToken);

            List<ResponseCustomerDTO> customerDTOs = customers.Adapt<List<ResponseCustomerDTO>>();

            return customerDTOs;
        }
    }
}
