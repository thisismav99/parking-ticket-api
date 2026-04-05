using Application.Utilities.Extensions;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.Parking;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Applications.Parking.Command
{
    internal record class AddParkingCommand([Required] DateTime ParkingDateTime,
        DateTime? ExitDateTime,
        [Required] Guid VehicleId,
        [Required] Guid EmployeeId,
        [Required] Guid TransactionId,
        [Required, MaxLength(50)] string CreatedBy,
        [Required] bool IsActive) : IRequest<Result<Guid>>;

    internal class AddParkingCommandHandler : IRequestHandler<AddParkingCommand, Result<Guid>>
    {
        private readonly IParkingService _parkingService;

        public AddParkingCommandHandler(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        public async Task<Result<Guid>> Handle(AddParkingCommand request, CancellationToken cancellationToken)
        {
            var errors = ValidatorHelper<AddParkingCommand>.Errors(request);
            bool hasErrors = !string.IsNullOrEmpty(errors);

            if (hasErrors)
            {
                return Result.Failure<Guid>(errors);
            }

            var parking = new Domain.Entities.Parking.Parking
            {
                ParkDateTime = request.ParkingDateTime,
                ExitDateTime = request.ExitDateTime,
                VehicleId = request.VehicleId,
                EmployeeId = request.EmployeeId,
                TransactionId = request.TransactionId,
                CreatedBy = request.CreatedBy,
                IsActive = request.IsActive
            };

            var result = await _parkingService.AddParking(parking, cancellationToken);

            if (result == Guid.Empty)
            {
                return Result.Failure<Guid>("Failed to add parking.");
            }

            return Result.Success(result);
        }
    }
}
