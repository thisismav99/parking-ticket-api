using Application.Utilities.Extensions;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.Customer;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Customer.Command
{
    internal record AddCustomerCommand([Required, MaxLength(50)] string FirstName,
        [MaxLength(50)] string? MiddleName,
        [Required, MaxLength(50)] string LastName,
        [MaxLength(15)] string? ContactNo,
        [MaxLength(100)] string? Email,
        int? LotNo,
        [Required, MaxLength(50)] string Street,
        [Required, MaxLength(50)] string Barangay,
        [Required, MaxLength(50)] string Municipality,
        [Required, MaxLength(50)] string Region,
        [Required, MaxLength(100)] string Country,
        [Required, MaxLength(100)] string CreatedBy,
        [Required] bool IsActive
        ) : IRequest<Result<Guid>>;

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

            var address = new Domain.Entities.Common.Address(request.LotNo,
                request.Street,
                request.Barangay,
                request.Municipality,
                request.Region,
                request.Country,
                request.CreatedBy,
                request.IsActive);

            var customer = new Domain.Entities.Customer.Customer(request.FirstName,
                request.MiddleName,
                request.LastName,
                request.ContactNo,
                request.Email,
                address,
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
