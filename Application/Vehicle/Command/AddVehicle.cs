using Application.Utilities.Extensions;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.Vehicle;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Vehicle.Command
{
    internal record AddVehicleCommand([Required, MaxLength(10)] string PlateNo,
        [Required, MaxLength(50)] string Brand,
        [MaxLength(30)] string? Color,
        [MaxLength(50)] string? Model,
        [Required] bool IsElectric,
        [Required] bool IsHybrid,
        Guid? CustomerId,
        [Required, MaxLength(100)] string CreatedBy,
        [Required] bool IsActive) : IRequest<Result<Guid>>;

    internal class AddVehicleCommandHandler : IRequestHandler<AddVehicleCommand, Result<Guid>>
    {
        private readonly IVehicleService _vehicleService;

        public AddVehicleCommandHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<Result<Guid>> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
        {
            var errors = ValidatorHelper<AddVehicleCommand>.Errors(request);
            bool hasErrors = !string.IsNullOrEmpty(errors);

            if (hasErrors)
            {
                return Result.Failure<Guid>(errors);
            }

            var vehicle = new Domain.Entities.Vehicle.Vehicle(request.PlateNo,
                request.Brand,
                request.Color,
                request.Model,
                request.IsElectric,
                request.IsHybrid,
                request.CustomerId,
                request.CreatedBy,
                request.IsActive);

            var result = await _vehicleService.AddVehicle(vehicle, cancellationToken);

            if(result == Guid.Empty)
            {
                return Result.Failure<Guid>($"Error saving vehicle: {request.Brand} {request.Model}");
            }

            return Result.Success(result);
        }
    }
}
