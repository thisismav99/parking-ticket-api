using Application.Applications.Common.DTO;
using Infrastructure.Interfaces.Common;
using Mapster;
using MediatR;

namespace Application.Applications.Common.Query
{
    internal record GetAddressesQuery(int PageNumber, int PageSize) : IRequest<List<ResponseAddressDTO>>;

    internal class GetAddressesQueryHandler : IRequestHandler<GetAddressesQuery, List<ResponseAddressDTO>>
    {
        private readonly IAddressService _addressService;

        public GetAddressesQueryHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<List<ResponseAddressDTO>> Handle(GetAddressesQuery request, CancellationToken cancellationToken)
        {
            var addresses = await _addressService.GetAddresses(request.PageNumber, request.PageSize, cancellationToken);

            List<ResponseAddressDTO> addressDTOs = addresses.Adapt<List<ResponseAddressDTO>>();

            return addressDTOs;
        }
    }
}
