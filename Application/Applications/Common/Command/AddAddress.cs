using Application.Utilities.Extensions;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Applications.Common.Command
{
    internal record AddAddressCommand(int? LotNo,
        [Required, MaxLength(50)] string Street,
        [Required, MaxLength(50)] string Barangay,
        [Required, MaxLength(50)] string Municipality,
        [Required, MaxLength(50)] string Region,
        [Required, MaxLength(100)] string Country,
        [Required, MaxLength(100)] string CreatedBy,
        [Required] bool IsActive) : IRequest<Result<Guid>>;

    internal class AddAddressCommandHandler : IRequestHandler<AddAddressCommand, Result<Guid>>
    {
        private readonly IAddressService _addressService;

        public AddAddressCommandHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<Result<Guid>> Handle(AddAddressCommand request, CancellationToken cancellationToken)
        {
            var errors = ValidatorHelper<AddAddressCommand>.Errors(request);
            bool hasErrors = !string.IsNullOrEmpty(errors);

            if(hasErrors)
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

            var result = await _addressService.AddAddress(address, cancellationToken);

            if(result == Guid.Empty)
            {
                return Result.Failure<Guid>("Error saving address.");
            } 

            return Result.Success(result);
        }
    }
}
