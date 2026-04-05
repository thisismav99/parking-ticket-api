using Application.Applications.Parking.DTO;
using Infrastructure.Interfaces.Parking;
using Mapster;
using MediatR;

namespace Application.Applications.Parking.Query
{
    internal record class GetParkingsQuery(int PageNumber, int PageSize) : IRequest<List<ResponseParkingDTO>>;

    internal class GetParkingsQuer : IRequestHandler<GetParkingsQuery, List<ResponseParkingDTO>>
    {
        private readonly IParkingService _parkingService;

        public GetParkingsQuer(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        public async Task<List<ResponseParkingDTO>> Handle(GetParkingsQuery request, CancellationToken cancellationToken)
        {
            var parkings = await _parkingService.GetParkings(request.PageNumber, request.PageSize, cancellationToken);

            List<ResponseParkingDTO> parkingDTOs = parkings.Adapt<List<ResponseParkingDTO>>();

            return parkingDTOs;
        }
    }
}
