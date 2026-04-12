using Application.Applications.Vehicle.DTO;
using Application.Utilities.Helpers;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.Vehicle;
using MediatR;

namespace Application.Applications.Vehicle.Command
{
    internal record AddVehicleCommand(AddVehicleDTO AddVehicleDTO) : IRequest<Result<Guid>>;

    internal class AddVehicleCommandHandler : IRequestHandler<AddVehicleCommand, Result<Guid>>
    {
        private readonly IVehicleService _vehicleService;

        public AddVehicleCommandHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<Result<Guid>> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = new Domain.Entities.Vehicle.Vehicle(request.AddVehicleDTO.PlateNo,
                request.AddVehicleDTO.Brand,
                request.AddVehicleDTO.Color,
                request.AddVehicleDTO.Model,
                request.AddVehicleDTO.IsElectric,
                request.AddVehicleDTO.IsHybrid,
                request.AddVehicleDTO.CustomerId,
                request.AddVehicleDTO.CreatedBy,
                request.AddVehicleDTO.IsActive);

            var result = await _vehicleService.AddVehicle(vehicle, cancellationToken);

            if(result == Guid.Empty)
            {
                return Result.Failure<Guid>(GetError.Error("vehicle"));
            }

            return Result.Success(result);
        }
    }
}
