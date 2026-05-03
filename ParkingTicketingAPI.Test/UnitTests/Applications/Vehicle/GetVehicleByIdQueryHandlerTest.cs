using Application.Applications.Vehicle.DTO;
using Application.Applications.Vehicle.Query;
using Application.Utilities.Helpers;
using Mapster;
using ParkingTicketingAPI.Test.UnitTests.Applications.Vehicle.Fixture;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Vehicle
{
    public sealed class GetVehicleByIdQueryHandlerTest : IClassFixture<VehicleFixture>
    {
        private readonly VehicleFixture _vehicleFixture;

        public GetVehicleByIdQueryHandlerTest(VehicleFixture vehicleFixture)
        {
            _vehicleFixture = vehicleFixture;
        }

        [Theory(DisplayName = "Vehicle should be returned when a valid vehicle id is entered and not found when invalid")]
        [InlineData("3f9b2a6c-8d4e-4f71-9c2a-5e7b1a93d6f8")]
        [InlineData("4f9b2a6c-8d4e-4f71-9c2a-5e7b1a93d6f9")]
        public async Task GetVehicleByIdQueryHandlerReturns(string guid)
        {
            // Arrange
            Guid vehicleId = Guid.Parse(guid);
            _vehicleFixture.SetupValidGetByIdVehicle();
            _vehicleFixture.SetupInvalidGetByIdVehicle();

            // Act
            var sut = new GetVehicleByIdQueryHandler(_vehicleFixture.MockVehicleService().Object);

            var result = await sut.Handle(new GetVehicleByIdQuery(vehicleId), CancellationToken.None);
            result.Adapt<ResponseVehicleDTO>();

            // Assert
            if (vehicleId == _vehicleFixture.GetVehicleGuid())
            {
                Assert.True(result.IsSuccess);
                Assert.NotNull(result.Value);
                Assert.IsType<ResponseVehicleDTO>(result.Value);
                _vehicleFixture.VerifyValidGetByIdVehicle();
            }
            else
            {
                Assert.True(result.IsFailure);
                Assert.Equal(GetError.NotFound(vehicleId), result.Error);
                Assert.IsType<string>(result.Error);
                _vehicleFixture.VerifyInvalidGetByIdVehicle();
            }
        }
    }
}
