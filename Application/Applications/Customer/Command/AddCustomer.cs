using Application.Utilities.Extensions;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.Customer;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Applications.Customer.Command
{
    internal record AddCustomerCommand([Required, MaxLength(50)] string FirstName,
        [MaxLength(50)] string? MiddleName,
        [Required, MaxLength(50)] string LastName,
        [MaxLength(15)] string? ContactNo,
        [MaxLength(100)] string? Email,
        [Required] Guid AddressId,
        [Required, MaxLength(100)] string CreatedBy,
        [Required] bool IsActive) : IRequest<Result<Guid>>;

    internal class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, Result<Guid>>
    {
        private readonly ICustomerService _customerService;

        public AddCustomerCommandHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<Result<Guid>> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            var errors = ValidatorHelper<AddCustomerCommand>.Errors(request);
            bool hasErrors = !string.IsNullOrEmpty(errors);

            if (hasErrors)
            {
                return Result.Failure<Guid>(errors);
            }

            var customer = new Domain.Entities.Customer.Customer(request.FirstName,
                request.MiddleName,
                request.LastName,
                request.ContactNo,
                request.Email,
                request.AddressId,
                request.CreatedBy,
                request.IsActive);

            var result = await _customerService.AddCustomer(customer, cancellationToken);

            if (result == Guid.Empty)
            {
                return Result.Failure<Guid>("Error saving customer.");
            }

            return Result.Success(result);
        }
    }
}
