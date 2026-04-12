using Application.Applications.Parking.DTO;
using Application.Utilities.Helpers;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.Parking;
using MediatR;

namespace Application.Applications.Parking.Command
{
    internal record class AddParkingCommand(AddParkingDTO AddParkingDTO) : IRequest<Result<Guid>>;

    internal class AddParkingCommandHandler : IRequestHandler<AddParkingCommand, Result<Guid>>
    {
        private readonly IParkingService _parkingService;

        public AddParkingCommandHandler(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        public async Task<Result<Guid>> Handle(AddParkingCommand request, CancellationToken cancellationToken)
        {
            var parking = new Domain.Entities.Parking.Parking(request.AddParkingDTO.ParkDateTime,
                request.AddParkingDTO.ExitDateTime,
                request.AddParkingDTO.VehicleId,
                request.AddParkingDTO.EmployeeId,
                request.AddParkingDTO.TransactionId,
                request.AddParkingDTO.CreatedBy,
                request.AddParkingDTO.IsActive);

            var result = await _parkingService.AddParking(parking, cancellationToken);

            if (result == Guid.Empty)
            {
                return Result.Failure<Guid>(GetError.Error("parking"));
            }

            return Result.Success(result);
        }
    }
}
