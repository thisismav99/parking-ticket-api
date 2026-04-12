using Application.Applications.Common.DTO;
using Application.Utilities.Helpers;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.Common;
using MediatR;

namespace Application.Applications.Common.Command
{
    internal record AddAddressCommand(AddAddressDTO AddAddressDTO) : IRequest<Result<Guid>>;

    internal class AddAddressCommandHandler : IRequestHandler<AddAddressCommand, Result<Guid>>
    {
        private readonly IAddressService _addressService;

        public AddAddressCommandHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<Result<Guid>> Handle(AddAddressCommand request, CancellationToken cancellationToken)
        {
            var address = new Domain.Entities.Common.Address(request.AddAddressDTO.LotNo,
                request.AddAddressDTO.Street,
                request.AddAddressDTO.Barangay,
                request.AddAddressDTO.Municipality,
                request.AddAddressDTO.Region,
                request.AddAddressDTO.Country,
                request.AddAddressDTO.CreatedBy,
                request.AddAddressDTO.IsActive);

            var result = await _addressService.AddAddress(address, cancellationToken);

            if(result == Guid.Empty)
            {
                return Result.Failure<Guid>(GetError.Error("address"));
            } 

            return Result.Success(result);
        }
    }
}
