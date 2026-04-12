using Application.Applications.Vehicle.DTO;
using Application.Utilities.Helpers;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.Vehicle;
using Mapster;
using MediatR;

namespace Application.Applications.Vehicle.Query
{
    internal record GetVehicleByIdQuery(Guid VehicleId) : IRequest<Result<ResponseVehicleDTO?>>;
    
    internal class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, Result<ResponseVehicleDTO?>>
    {
        private readonly IVehicleService _vehicleService;

        public GetVehicleByIdQueryHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<Result<ResponseVehicleDTO?>> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleService.GetVehicleById(request.VehicleId, cancellationToken);

            if(vehicle is null)
            {
                return Result.Failure<ResponseVehicleDTO?>(GetError.NotFound(request.VehicleId));
            }

            ResponseVehicleDTO? vehicleDTO = vehicle.Adapt<ResponseVehicleDTO?>();

            return Result.Success(vehicleDTO);
        }
    }
}
