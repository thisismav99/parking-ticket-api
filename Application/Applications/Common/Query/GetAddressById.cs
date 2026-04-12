using Application.Applications.Common.DTO;
using Application.Utilities.Helpers;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.Common;
using Mapster;
using MediatR;

namespace Application.Applications.Common.Query
{
    internal record GetAddressByIdQuery(Guid AddressId) : IRequest<Result<ResponseAddressDTO?>>;

    internal class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQuery, Result<ResponseAddressDTO?>>
    {
        private readonly IAddressService _addressService;

        public GetAddressByIdQueryHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<Result<ResponseAddressDTO?>> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
        {
            var address = await _addressService.GetAddressById(request.AddressId, cancellationToken);

            if(address is null)
            {
                return Result.Failure<ResponseAddressDTO?>(GetError.NotFound(request.AddressId));
            }

            ResponseAddressDTO? addressDTO = address.Adapt<ResponseAddressDTO?>();

            return Result.Success(addressDTO);
        }
    }
}
