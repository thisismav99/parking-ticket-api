using Application.Applications.Customer.DTO;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.Customer;
using Mapster;
using MediatR;

namespace Application.Applications.Customer.Query
{
    internal record class GetCustomerByIdQuery(Guid CustomerId) : IRequest<Result<ResponseCustomerDTO?>>;

    internal class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Result<ResponseCustomerDTO?>>
    {
        private readonly ICustomerService _customerService;

        public GetCustomerByIdQueryHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<Result<ResponseCustomerDTO?>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerService.GetCustomerById(request.CustomerId, cancellationToken);

            if (customer is null)
            {
                return Result.Failure<ResponseCustomerDTO?>($"No customer found for Id: {request.CustomerId}");
            }

            ResponseCustomerDTO? customerDTO = customer.Adapt<ResponseCustomerDTO?>();

            return Result.Success(customerDTO);
        }
    }
}
