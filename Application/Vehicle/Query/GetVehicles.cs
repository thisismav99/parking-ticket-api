using Application.Vehicle.DTO;
using Infrastructure.Services.Vehicle;
using Mapster;
using MediatR;

namespace Application.Vehicle.Query
{
    internal class GetVehiclesQuery : IRequest<List<ResponseVehicleDTO>>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }

    internal class GetVehiclesQueryHandler : IRequestHandler<GetVehiclesQuery, List<ResponseVehicleDTO>>
    {
        private readonly IVehicleService _vehicleService;

        public GetVehiclesQueryHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<List<ResponseVehicleDTO>> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleService.GetVehicles(request.PageNumber, request.PageSize);

            List<ResponseVehicleDTO> vehicleDTOs = vehicles.Adapt<List<ResponseVehicleDTO>>();

            return vehicleDTOs;
        }
    }
}
