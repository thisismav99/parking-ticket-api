using Application.Applications.Parking.DTO;
using Application.Utilities.Helpers;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.Parking;
using Mapster;
using MediatR;

namespace Application.Applications.Parking.Query
{
    internal record class GetParkingByIdQuery(Guid ParkingId) : IRequest<Result<ResponseParkingDTO?>>;

    internal class GetParkingByIdQueryHandler : IRequestHandler<GetParkingByIdQuery, Result<ResponseParkingDTO?>>
    {
        private readonly IParkingService _parkingService;

        public GetParkingByIdQueryHandler(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        public async Task<Result<ResponseParkingDTO?>> Handle(GetParkingByIdQuery request, CancellationToken cancellationToken)
        {
            var parking = await _parkingService.GetParkingById(request.ParkingId, cancellationToken);

            if(parking is null)
            {
                return Result.Failure<ResponseParkingDTO?>(GetError.NotFound(request.ParkingId));
            }

            ResponseParkingDTO? parkingDTO = parking.Adapt<ResponseParkingDTO?>();

            return Result.Success(parkingDTO);
        }
    }
}
