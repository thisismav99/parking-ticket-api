using Application.Vehicle.DTO;
using CSharpFunctionalExtensions;
using Infrastructure.Services.Vehicle;
using MediatR;

namespace Application.Vehicle.Command
{
    internal class AddVehicleCommand : IRequest<Result<Guid>>
    {
        public required AddVehicleDTO AddVehicle { get; set; }
    }

    internal class AddVehicleCommandHandler : IRequestHandler<AddVehicleCommand, Result<Guid>>
    {
        private readonly IVehicleService _vehicleService;

        public AddVehicleCommandHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<Result<Guid>> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = new Domain.Entities.Vehicle.Vehicle(request.AddVehicle.PlateNo,
                request.AddVehicle.Brand,
                request.AddVehicle.Color,
                request.AddVehicle.Model,
                request.AddVehicle.IsElectric,
                request.AddVehicle.IsHybrid,
                request.AddVehicle.CustomerId,
                request.AddVehicle.CreatedBy,
                request.AddVehicle.IsActive);

            return await _vehicleService.AddVehicle(vehicle);
        }
    }
}
