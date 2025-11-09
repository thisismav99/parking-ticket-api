using Application.Vehicle.DTO;
using Infrastructure.Interfaces.Vehicle;
using Mapster;
using MediatR;

namespace Application.Vehicle.Query
{
    internal record GetVehiclesQuery(int PageNumber, int PageSize) : IRequest<List<ResponseVehicleDTO>>;

    internal class GetVehiclesQueryHandler : IRequestHandler<GetVehiclesQuery, List<ResponseVehicleDTO>>
    {
        private readonly IVehicleService _vehicleService;

        public GetVehiclesQueryHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<List<ResponseVehicleDTO>> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleService.GetVehicles(request.PageNumber, request.PageSize, cancellationToken);

            List<ResponseVehicleDTO> vehicleDTOs = vehicles.Adapt<List<ResponseVehicleDTO>>();

            return vehicleDTOs;
        }
    }
}
