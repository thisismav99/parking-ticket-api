using Application.Vehicle.DTO;
using CSharpFunctionalExtensions;
using Infrastructure.Services.Vehicle;
using Mapster;
using MediatR;

namespace Application.Vehicle.Query
{
    internal class GetVehicleByIdQuery : IRequest<Result<ResponseVehicleDTO?>>
    {
        public Guid VehicleId { get; set; }
    }
    
    internal class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, Result<ResponseVehicleDTO?>>
    {
        private readonly IVehicleService _vehicleService;

        public GetVehicleByIdQueryHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<Result<ResponseVehicleDTO?>> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleService.GetVehicleById(request.VehicleId);

            if(vehicle.IsFailure)
            {
                return Result.Failure<ResponseVehicleDTO?>(vehicle.Error);
            }

            ResponseVehicleDTO? vehicleDTO = vehicle.Adapt<ResponseVehicleDTO?>();

            return Result.Success(vehicleDTO);
        }
    }
}
