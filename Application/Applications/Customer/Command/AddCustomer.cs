using Application.Applications.Customer.DTO;
using Application.Utilities.Helpers;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.Customer;
using MediatR;

namespace Application.Applications.Customer.Command
{
    internal record AddCustomerCommand(AddCustomerDTO AddCustomerDTO) : IRequest<Result<Guid>>;

    internal class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, Result<Guid>>
    {
        private readonly ICustomerService _customerService;

        public AddCustomerCommandHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<Result<Guid>> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Domain.Entities.Customer.Customer(request.AddCustomerDTO.FirstName,
                request.AddCustomerDTO.MiddleName,
                request.AddCustomerDTO.LastName,
                request.AddCustomerDTO.ContactNo,
                request.AddCustomerDTO.Email,
                request.AddCustomerDTO.AddressId,
                request.AddCustomerDTO.CreatedBy,
                request.AddCustomerDTO.IsActive);

            var result = await _customerService.AddCustomer(customer, cancellationToken);

            if (result == Guid.Empty)
            {
                return Result.Failure<Guid>(GetError.Error("customer"));
            }

            return Result.Success(result);
        }
    }
}
